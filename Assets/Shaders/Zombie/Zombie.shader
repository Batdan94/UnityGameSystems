Shader "Custom/Zombie" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)


		_Tess ("Tessellation", Range(1,32)) = 4
		_MainTex("Base Texture", 2D) = "white" {}

		_DispTex("Disp Texture", 2D) = "gray" {}
		_NormalTex("Normal Texture", 2D) = "bump" {}
		_Displacement("Displacement", Range (0.0, 5.0)) = 0.3
		_Tiling("Tiling", float) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessFixed 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;

		};

		float _Tess;

		float4 tessFixed()
		{
			return _Tess;
		}

		sampler2D _MainTex;
		sampler2D _DispTex;
		float _Displacement;
		float _Tiling;


		void disp (inout appdata v)
		{
			float d = tex2Dlod(_DispTex, float4(v.texcoord.xy*_Tiling, 0, 0)).r * _Displacement;
			v.vertex.xyz += v.normal * d;
		}

		struct Input {
			float2 uv_MainTex;
		};






		sampler2D _NormalMap;
		fixed4 _Color;

		//UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		//UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
			o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
