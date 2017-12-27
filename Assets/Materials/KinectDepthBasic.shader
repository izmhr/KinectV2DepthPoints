Shader "Custom/KinectDepthBasic"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Displacement("Displacement", Range(0, 0.1)) = 0.03
		_Color("Particle Color", Color) = (1,1,1,1)
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
				float4 col : COLOR;
			};

			// 使わない
			float rand(float2 co) {
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			};

			float3 HUEtoRGB(in float  H)
			{
				float R = abs(H * 6 - 3) - 1;
				float G = 2 - abs(H * 6 - 2);
				float B = 2 - abs(H * 6 - 4);
				return saturate(float3(R, G, B));
			}

			float3 HSVtoRGB(in float3 HSV)
			{
				float3 RGB = HUEtoRGB(HSV.x);
				return ((RGB - 1) * HSV.y + 1) * HSV.z;
			}

			sampler2D _MainTex;
			float _Displacement;
			fixed4 _Color;

			float4 _MainTex_ST;


			v2f vert(appdata v)
			{
				// ピクセル毎の色情報に乗せてきたデプス情報を復元する
				float4 col = tex2Dlod(_MainTex, float4(v.uv, 0, 0));
				
				//TextureFormat.RGBA4444の場合
				//float d = (col.w + col.z * 16 + col.y * 16 * 16 + col.x * 16 * 16 * 16) * _Displacement;
				//TextureFormat.ARGB4444の場合
				//float d = (col.z + col.y * 16 + col.x * 16 * 16 + col.w * 16 * 16 * 16) * _Displacement;
				
				//TextureFormat.R16の場合
				float d = col.x * 4000 * _Displacement;

				// デプスカメラ座標系から空間に展開する。
				// C#の層でやるならCoordinateMapper.MapDepthFrameToCameraSpace を用いる
				v.vertex.x = v.vertex.x * d / 3.656;
				v.vertex.y = v.vertex.y * d / 3.656;
				v.vertex.z = d;
				
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//今回はvertexとfragmentが一致するようにメッシュを用意しているので、そのまま渡す、と指示しても良い。
				//o.uv = v.uv;

				// 距離に応じた色をつけてあげる
				float3 hsv = float3(1, 1, 1);
				hsv.x = v.vertex.z % 1.0;
				float3 rgb = HSVtoRGB(hsv);
				o.col = float4(rgb.xyz, 1);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// テクスチャの色をそのまま使う
				//fixed4 col = tex2D(_MainTex, i.uv);
				// 指定の色にする場合
				//fixed4 col = _Color;
				fixed4 col = i.col;

				return col;
			}
			ENDCG
		}
	}
}