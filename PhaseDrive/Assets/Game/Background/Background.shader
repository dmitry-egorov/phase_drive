// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Skybox/Background"
{
	Properties
	{
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Assets/Shader Tools/Common.cginc"
			#include "BackgroundFragment.cginc"

			m4 _CameraTransform;
	        v1 _LocalTime;

			struct v2f
			{
				float4  pos : SV_POSITION;
				float4 rops : TEXCOORD0;  // ray origin, w = pixel size
				float4  sun : TEXCOORD1; // sun parameters (xy: sun flare screen position, z: sun's overall visibility, w: degree of the sunset)
			};

			v2f vert(float4 v:POSITION)
			{
				v2f o;

				v2 rs = _ScreenParams.xy; // screen resolution
				v4 fc = UnityObjectToClipPos(v);
				o.pos = fc;

				fc.y = rs.y - fc.y;

				v2 sp = screen_point(fc.xy, rs);

				m3 C = (m3)_CameraTransform; // camera transform
				v3 ro = _CameraTransform[3].xyz; // ray origin
				//v1 dst = _CameraDistance;
				//v3 ro = v3(0, 0, -dst);
				//m3 C = (m3)unity_CameraToWorld; // camera transform
				
				v1 ft = _CameraFovTan;
				v1 ps = angular_pixel_size(rs, ft); // pixel size
				v2 fsp; // sun flare screen position
				v1 sov; // sun's overall visibility
				v1 ssd; // degree of the sunset
				sun_visibility(ro, C, ft, rs.x / rs.y, sov, ssd, fsp);

				o.rops = v4(ro, ps);
				o.sun  = v4(fsp, sov, ssd);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
            {
				v2 fc = i.pos.xy; // frag coordinate
				v2 rs = _ScreenParams.xy; // screen resolution
				fc.y = rs.y-fc.y;

				m3 C = (m3)_CameraTransform; // camera transform
				//m3 C = (m3)unity_CameraToWorld; // camera transform
				v2 sp = screen_point(fc.xy, rs);
				v1 ft = _CameraFovTan;
				v3 rd = screen_to_ray(sp, C, ft); // ray direction

				v3 ro = i.rops.xyz;
				v1 ps = i.rops.w;
				v2 fsp = i.sun.xy; // sun flare screen position
				v1 sov = i.sun.z;  // sun's overall visibility
				v1 ssd = i.sun.w;  // degree of the sunset
				v1 t = _LocalTime;

				return background(fc, sp, ro, rd, ps, t, sov, ssd, fsp);
            }
            ENDCG
        }
    }
}
