Shader "UI/Circle"
{
    Properties
    {
        _Radius("Radius", Float) = 1.0
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_WidthPx("WidthPx", Float) = 4.0
        _BackColor("BackColor", Color) = (0.0, 0.0, 0.0, 1.0)
        _BackWidthPx("BacksWidthPx", Float) = 6.0
	}
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            float _Radius;
            fixed4 _Color;
            float _WidthPx;
            fixed4 _BackColor;
            float _BackWidthPx;

            fixed4 frag (v2f i) : SV_Target
            {
                v2 cuv = 2.0 * i.uv - 1.0; // centered uv
                v1 uv2 = vsqr(cuv);
                v1 r2 = sqr(_Radius);
                v1 fw = fwidth(i.uv);
                v1 w = _WidthPx * fw;
                v1 bw = _BackWidthPx * fw;



                v1 cm = smoothstep(uv2 - w, uv2, r2)
                      - smoothstep(uv2, uv2 + w, r2); //color mix

                v1 a = smoothstep(uv2 - bw, uv2, r2)
                     - smoothstep(uv2, uv2 + bw, r2); // alpha

                //a = 1.0;

                v4 c = mix(_BackColor, _Color, cm);
                c.w = a;

                return c;

                //return fixed4(i.uv.r, i.uv.g, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
