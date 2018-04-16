Shader "Custom/002" {
	Properties {
	_MainTex ("c", 2D) = "white" {}
	_MainTex1 ("d", 2D) = "white" {}
	_Dark ("Noise", Range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Opaque"="Transparent" }
	

		CGPROGRAM
		
		#pragma surface surf Standard alpha:fade

		sampler2D _MainTex;
		sampler2D _MainTex1;
		float _Dark;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex1;
		};

	


		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 d = tex2D (_MainTex1, float2(IN.uv_MainTex1.x, IN.uv_MainTex1.y - _Time.y)) ;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + d.r) ;
			//fixed4 d = tex2D (_MainTex1, float2 (IN.uv_MainTex1.x, IN.uv_MainTex1.y - _Time.y) ) ;
			float Dark = d + (_Dark, _Dark, _Dark);
			o.Emission = c.rgb ;
			o.Alpha =c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
