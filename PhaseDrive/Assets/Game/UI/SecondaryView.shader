Shader "UI/SecondaryView"
{

    Properties
    {
        _MainTex("Texture", 2D) = "white" { }
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_WidthPx("WidthPx", Float) = 4.0
        _BackColor("BackColor", Color) = (0.0, 0.0, 0.0, 1.0)
        _BackWidthPx("BacksWidthPx", Float) = 6.0
	}
    SubShader
    {
        Tags {"RenderType" = "Opaque" }
        LOD 100

        ZWrite On

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "Assets/Shader Tools/Common.hlsl"

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _Color;
            float _WidthPx;
            fixed4 _BackColor;
            float _BackWidthPx;

            fixed4 frag (v2f i) : SV_Target
            {
                v1 fw = fwidth(i.uv);
                v1 w = _WidthPx * fw;
                v1 bw = _BackWidthPx * fw;

                v1 cm = 
                    pulse3(i.uv.x, 0.0, w)
                  + pulse3(i.uv.y, 0.0, w)
                  + pulse3(i.uv.x, 1.0, w)
                  + pulse3(i.uv.y, 1.0, w)
                ; //color mix

                v4 c = mix(_BackColor, _Color, sat1(cm));

                fixed4 tc = tex2D(_MainTex, i.uv);
                v1 tm = 
                      pulse3(i.uv.x, 0.0, bw)
                    + pulse3(i.uv.y, 0.0, bw)
                    + pulse3(i.uv.x, 1.0, bw)
                    + pulse3(i.uv.y, 1.0, bw)
                    ;
                ; //texture mix

                c = mix(tc, c, sat1(tm));

                return c;
            }
            ENDCG
        }
    }
}
