Shader "Custom/TerrainShader" {
	Properties{
	    _BaseTex ("Base Texture", 2D) = "white" {}
	    _TopTex ("Top Texture", 2D) = "white" {}
	    _SandTex ("Sand Texture", 2D) = "white" {}
        _HeightMin ("Height Min", Float) = -1
        _HeightMax ("Height Max", Float) = 1
        _Colour1 ("Colour 1", Color) = (0,0,0,1)
        _Colour2 ("Colour 2", Color) = (1,1,1,1)
        _Colour3 ("Colour 3", Color) = (1,1,1,1)
		_MIN ("MIN", Float) = 0
		_MAX ("MAX", Float) = 0
		_Height1 ("Height 1", Float) = 1
		_Height2 ("Height 2", Float) = 1
	    _Height3 ("Height 3", Float) = 1
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		//#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _BaseTex;
		sampler2D _TopTex;
		sampler2D _SandTex;
        fixed4 _Colour1;
        fixed4 _Colour2;
        fixed4 _Colour3;
        float _HeightMin;
        float _HeightMax;
		float _MIN;
		float _MAX;
		float _Height1;
		float _Height2;
		float _Height3;

        
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			if (IN.worldPos.y >= _Height1)
				//o.Albedo = tex2D (_TopTex, IN.uv_MainTex).rgb;
				o.Albedo = _Colour1.rgb;
			//if ((IN.worldPos.y <= _Height1 + 0.2) & (IN.worldPos.y >= _Height1 - 0.2))

				//fixed4 transition1 = lerp(_Colour1.rgba, _Colour2.rgba, 0.5);
				//o.Albedo = transition1.rgb;

			if ((IN.worldPos.y <= _Height1) & (IN.worldPos.y >= _Height2))
				//o.Albedo = tex2D (_BaseTex, IN.uv_MainTex).rgb;
				o.Albedo = _Colour2.rgb;
			if (IN.worldPos.y <= _Height2)
				//o.Albedo = tex2D (_SandTex, IN.uv_MainTex).rgb;
				o.Albedo = _Colour3.rgb;
			//float heightPercent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);
			//o.Albedo = float3(0, 1, 0);
			//float3 localPos = IN.worldPos -  mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
			/*half4 c = tex2D (_MainTex, IN.uv_MainTex);
			float h = (_HeightMax-IN.worldPos.y) / (_HeightMax-_HeightMin);
			fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);
			//o.Albedo = c.rgba * tintColor.rgb;
			o.Albedo = tintColor.rgb;
			o.Alpha = tintColor.a;*/
			//o.Alpha = 0;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
