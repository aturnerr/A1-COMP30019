﻿// Original Cg/HLSL code stub copyright (c) 2010-2012 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
// Adapted further by Chris Ewin, 23 Sep 2013
// Adapted further (again) by Alex Zable (port to Unity), 19 Aug 2016

//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/PhongShader"
{
	Properties
	{   
	    // colour of the light source
		_PointLightColor("Point Light Color", Color) = (0, 0, 0, 1)
		// position of the light source
		_PointLightPosition("Point Light Position", Vector) = (0.0, 0.0, 0.0)
		// terrain colours
		_Colour1("Mountain top Colour", Color) = (0,0,0,1)
		_Colour2("Grass Colour", Color) = (1,1,1,1)
		_Colour3("Sand Colour", Color) = (1,1,1,1)
		_Colour4("Deep sand colour", Color) = (1,1,1,0)
		// height values of the terrain, set by the diamondsquare script
		_Height1("Height 1", Float) = 1
		_Height2("Height 2", Float) = 1
		_Height3("Height 3", Float) = 1
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float3 _PointLightColor;
			uniform float3 _PointLightPosition;
			fixed4 _Colour1;
			fixed4 _Colour2;
			fixed4 _Colour3;
			fixed4 _Colour4;
			float _Height1;
			float _Height2;
			float _Height3;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float4 worldVertex : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;

				// Convert Vertex position and corresponding normal into world coords.
				// Note that we have to multiply the normal by the transposed inverse of the world 
				// transformation matrix (for cases where we have non-uniform scaling; we also don't
				// care about the "fourth" dimension, because translations don't affect the normal) 
				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
				float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));

				// Transform vertex in world coordinates to camera coordinates, and pass colour
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = float4(1,1,1,1);
                //o.customColour = abs(v.normal.y);
				// Pass out the world vertex position and world normal to be interpolated
				// in the fragment shader (and utilised)
				o.worldVertex = worldVertex;
				o.worldNormal = worldNormal;

                // https://forum.unity.com/threads/change-surface-color-based-on-the-angle-between-surface-normal-and-world-up.355215/
                // calculate dot product for mountain slopes
                float x = dot(v.normal, v.normal.y);
                // between 0-1 range
                x = x * 0.5-0.5;
                // interpolate between the colour and a darker grey
                float4 slopeColour2 = lerp(_Colour1.rgba, float4(0.6,0.6,0.6,1), x+0.8);
                
                // transition interpolations
                fixed4 oceanTransition = lerp(_Colour3.rgba, _Colour4.rgba, (_Height2-1 - o.worldVertex.y)/2);
                fixed4 transition1 = lerp(_Colour1.rgba, _Colour2.rgba, ((_Height1+0.5 - o.worldVertex.y) / ((_Height1+0.5)-(_Height1-0.5))));
                fixed4 transition2 = lerp(_Colour2.rgba, _Colour3.rgba, ((_Height2+0.25 - o.worldVertex.y) / ((_Height2+0.25)-(_Height2-0.25))));
                
                // terrain colours
				if (o.worldVertex.y >= _Height1)
					o.color = slopeColour2;
			    // first transition segment, between the mountain tops and grass
				if ((o.worldVertex.y <= _Height1+0.5) & (o.worldVertex.y >= _Height1-0.5))
				    o.color = transition1;
				// grass segment
				if ((o.worldVertex.y <= _Height1-0.5) & (o.worldVertex.y >= _Height2+0.25))
					o.color = _Colour2;
                // second transition segment, between the grass and sand
				if ((o.worldVertex.y <= _Height2+0.25) & (o.worldVertex.y >= _Height2-0.25))
				    o.color = transition2;
                // sand segment
				if ((o.worldVertex.y <= _Height2-0.25) & (o.worldVertex.y >= _Height2-1))
					o.color = _Colour3;
                // third transition segment, darkens the sand as it goes deeper
				if (o.worldVertex.y <= _Height2-1)
					o.color = oceanTransition;

				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				// Our interpolated normal might not be of length 1
				float3 interpNormal = normalize(v.worldNormal);

				// Calculate ambient RGB intensities
				float Ka = 0.5;
				float3 amb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

				// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
				// (when calculating the reflected ray in our specular component)
				float fAtt = 1;
				float Kd = 1;
				float3 L = normalize(_PointLightPosition - v.worldVertex.xyz);
				float LdotN = dot(L, interpNormal);
				float3 dif = fAtt * _PointLightColor.rgb * Kd * v.color.rgb * saturate(LdotN);

				// Calculate specular reflections
				float Ks = 0;
				float specN = 5; // Values>>1 give tighter highlights
				float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
				// Using classic reflection calculation:
				//float3 R = normalize((2.0 * LdotN * interpNormal) - L);
				//float3 spe = fAtt * _PointLightColor.rgb * Ks * pow(saturate(dot(V, R)), specN);
				// Using Blinn-Phong approximation:
				specN = 25; // We usually need a higher specular power when using Blinn-Phong
				float3 H = normalize(V + L);
				float3 spe = fAtt * _PointLightColor.rgb * Ks * pow(saturate(dot(interpNormal, H)), specN);

				// Combine Phong illumination model components
				float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;
				returnColor.a = v.color.a;

				return returnColor;
			}
			ENDCG
		}
	}
}
