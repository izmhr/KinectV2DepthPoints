Shader "Custom/KinectDepthBasic"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Displacement("Displacement", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float rand(float2 co) {
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			};

			sampler2D _MainTex;
			//float4 _MainTex_ST;
			float _Displacement;

			v2f vert(appdata v)
			{
				// デプスのデータに基づいて、z方向に頂点を移動させる
				float4 col = tex2Dlod(_MainTex, float4(v.uv, 0, 0));
				float d = col.w + col.z * 16 + col.y * 16 * 16 + col.x * 16 * 16 * 16;
				v.vertex.z = d * _Displacement;
				
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}