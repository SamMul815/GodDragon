Shader "Custom/ice_reflection" {
	Properties {

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal ("Normal", 2D) = "white" {}
		_A ("A", 2D) = "" {}

	}
	SubShader {
		Tags { "RenderType"="Transparent"}


		CGPROGRAM
		#pragma surface surf Standard alpha: fade

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _A;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float3 worldRefl;
			INTERNAL_DATA	

		};




		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb*0.2;
			o.Normal = UnpackNormal (tex2D(_Normal, IN.uv_Normal));
			o.Emission = tex2D (_A, WorldReflectionVector(IN, o.Normal)).rgb*1.3;
			o.Alpha = c.a*0.1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
