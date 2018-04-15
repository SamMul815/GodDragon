Shader "Custom/002" {
	Properties {
	_MainTex ("texture(c)", 2D) = "white" {}
	_MainTex1 ("noise(d)", 2D) = "white" {}
	_Dark ("Noise", Range(0,1)) = 0
	_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Opaque"="Transparent" }
	

		CGPROGRAM
		
		#pragma surface surf Standard alpha:fade

		sampler2D _MainTex;
		sampler2D _MainTex1;
		float _Dark;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex1;
			float4 color: Color;
		};

	


		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 d = tex2D (_MainTex1, float2(IN.uv_MainTex1.x, IN.uv_MainTex1.y - _Time.y*2)) ;
			fixed4 c = tex2D (_MainTex,float2(IN.uv_MainTex.x, IN.uv_MainTex.y - _Time.y) + d.xy)*_Color ;
			//fixed4 d = tex2D (_MainTex1, float2 (IN.uv_MainTex1.x, IN.uv_MainTex1.y - _Time.y) ) ;
			//float Dark = d + (_Dark, _Dark, _Dark);
			o.Emission = c.rgb*5 ;
			o.Alpha =c.a*IN.color.a*d.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
