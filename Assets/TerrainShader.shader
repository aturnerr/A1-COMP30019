Shader "Custom/TerrainShader" {
	Properties{

	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float minHeight = DiamondSquare.MinHeight();
		float maxHeight = DiamondSquare.MaxHeight;

		struct Input {
			float3 worldPos;
		};

		float inverseLerp(float a, float b, float value) {
			return saturate((value - a) / (value - b));
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {

			//float heightPercent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);
			o.Albedo = float3(0, 1, 0);

		}
		ENDCG
	}
	FallBack "Diffuse"
}
