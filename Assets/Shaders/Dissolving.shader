// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BurningPaper"
{
	Properties{
		_MainTex("Main texture", 2D) = "white" {}
		_DissolveTex("Dissolution texture", 2D) = "gray" {}
		[PerRendererData]_Threshold("Threshold", Range(0, 1.1)) = 0
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}

		SubShader{

			Tags { 
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane" 
			}

			Blend SrcAlpha OneMinusSrcAlpha

			Pass {

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 pos : SV_POSITION;
					fixed4 color : COLOR;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				uniform fixed4 _Color;

				v2f vert(appdata_t  v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.color = v.color * _Color;
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}

				sampler2D _DissolveTex;
				float _Threshold;

				fixed4 frag(v2f i) : SV_Target {
					fixed4 color = i.color;
					color.a *= tex2D(_MainTex, i.uv).a;
					fixed dissolution = 1 - tex2D(_DissolveTex, i.uv).r;
					if (dissolution < _Threshold - 0.04)
					{
						discard;
					}

					bool dissolving = dissolution < _Threshold;
					float dissolving_state = lerp(1, 0, 1 - saturate(abs(_Threshold - dissolution) / 0.04));
					fixed4 dissolving_color = color * fixed4(1- dissolving_state, 1- dissolving_state,1- dissolving_state, 1);

					return lerp(color, dissolving_color, dissolving);
				}

				ENDCG

			}

		}
			FallBack "Diffuse"
}