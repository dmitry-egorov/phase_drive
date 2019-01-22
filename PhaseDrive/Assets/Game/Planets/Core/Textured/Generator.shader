Shader "Procedural Planet/Generator"
{
    Properties
    {
		_Offset("Offset", Float) = 0.0
		_CloudColor("Cloud Color", Color) = (1.0,1.0,1.0,1.0)
		_CloudLacunarity("Cloud Lacunarity", Float) = 2.01
		_CloudPersistence("Cloud Persistence", Float) = 0.5
		_CloudWarpFactor("Cloud Warp Factor", Float) = 0.5
		_ContinentColor("Continent Color", Color) = (1.0,1.0,1.0,1.0)
		_OceanColor("Ocean Color", Color) = (1.0,1.0,1.0,1.0)
		_LandLacunarity("Land Lacunarity", Float) = 2.01
		_LandPersistence("Land Persistence", Float) = 0.5
		_LandWarpFactor("Land Warp Factor", Float) = 0.5
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
			#include "Assets/Shader Tools/SimplexNoise.cginc"

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

			float3 simplex3(float3 p, int octaves, float lacunarity, float persistence)
			{
				return float3
				(
					simplex_fbm(p + float3(343.58, 154.63, 868.27), octaves, lacunarity, persistence),
					simplex_fbm(p + float3(623.87, 254.12, 168.84), octaves, lacunarity, persistence),
					simplex_fbm(p + float3(233.35, 104.24, 368.53), octaves, lacunarity, persistence)
				);
			}

			float warped_simplex(float3 p, float lacunarity, float persistence, float factor)
			{
				p += factor * simplex3(p, 4, lacunarity, persistence);
				p += factor * simplex3(p, 10, lacunarity, persistence);

				return simplex_fbm(p, 9, lacunarity, persistence);
			}

			float _Offset;
			float3 _CloudColor;
			float _CloudLacunarity;
			float _CloudPersistence;
			float _CloudWarpFactor;

			float3 _ContinentColor;
			float3 _OceanColor;
			float _LandLacunarity;
			float _LandPersistence;
			float _LandWarpFactor;

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

				float land = warped_simplex(sphere_point + _Offset, _LandLacunarity, _LandPersistence, _LandWarpFactor);

				float w = 60.*ddx(s.uv);
				float3 land_color = lerp(_OceanColor, _ContinentColor, smoothstep(-w, w, land));

				float per = 0.05*(simplex_fbm(sphere_point + float3(31.55, 31.55, 31.55), 10, _CloudLacunarity, _CloudPersistence)) + _CloudPersistence;
				float clouds = warped_simplex(sphere_point + 2 * _Offset + 34.323, _CloudLacunarity, per, _CloudWarpFactor);
				clouds = pow(smoothstep(0.1, 0.8, clouds), 0.5);

				float3 final_color = lerp
				(
					land_color,
					_CloudColor,
					clouds
				);

                return fixed4(final_color, 1.0);


				//float3 col = 0.5 * (simplex3(sphere_point, 12) + 1.0);
				//float3 land_color = lerp
				//(
				//    (_OceanColor - 0.2 * land),
				//    0.5 * (land + 1.0) * _ContinentColor,
				//    smoothstep(-w, w, land)
				//);
            }
            ENDCG
        }
    }
}
