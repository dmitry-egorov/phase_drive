Shader "Generation/CreatePlanet"
{
    Properties
    {
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "SimplexNoise.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 circle(float2 p, float2 c, float r)
            {
                return step(distance(p, c), r);
            }

            static float3 directions[6] =
            {
                float3(+0.0, +1.0, +0.0),
                float3(+0.0, -1.0, +0.0),
                float3(-1.0, +0.0, +0.0),
                float3(+1.0, +0.0, +0.0),
                float3(+0.0, +0.0, +1.0),
                float3(+0.0, +0.0, -1.0)
            };

            fixed4 frag (v2f s) : SV_Target
            {
                float2 scaled_uv = float2(s.uv.x * 6, s.uv.y);
                float i = floor(scaled_uv.x);
                float2 p = frac(scaled_uv);
                float3 local_up = directions[i];
                float3 axis1 = float3(local_up.y, local_up.z, local_up.x);
                float3 axis2 = cross(local_up, axis1);
                float3 cube_point = local_up + (p.x - .5) * 2 * axis1 + (p.y - .5) * 2. * axis2;
                float3 sphere_point = normalize(cube_point);

                //float3 col = circle(p, float2(0.5, 0.5), 0.1);
                float3 col = SimplexNormal(sphere_point, 11, float3(0.0, 0.0, 0.0), 1.0, 1.0, 2.01, 0.6);
                return fixed4(col, 1.0);
            }
            ENDCG
        }
    }
}
