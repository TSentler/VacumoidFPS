Shader "Custom/FastUnlit" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
//		[NoScaleOffset]
//		[HideInInspector]
//		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100
	
		Pass
		{
			CGPROGRAM
			#pragma multi_compile_instancing
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				fixed4 vertex : POSITION;
				fixed2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				fixed4 vertex : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			// sampler2D _MainTex;
			// fixed4 _MainTex_ST;

			UNITY_INSTANCING_BUFFER_START(Props)
				UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			UNITY_INSTANCING_BUFFER_END(Props)
			
			v2f vert (appdata v)
			{
				UNITY_SETUP_INSTANCE_ID(v)
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv;
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				// const fixed4 col = tex2D(_MainTex, i.uv);
				// return col * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
				return UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
			}
			ENDCG
		}	
	}
}