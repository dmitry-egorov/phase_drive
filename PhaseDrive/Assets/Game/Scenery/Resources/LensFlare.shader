Shader "Hidden/Custom/LensFlare"
{
	HLSLINCLUDE

	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
	#include "Assets/Shader Tools/Common.hlsl"
	#include "BackgroundCommon.hlsl"

	TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

	//----LENS FLARE----

	m4 _CameraTransform;


	// thanks, internets

	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float4 sun : TEXCOORD1; // sun parameters (xy: sun flare screen position, z: sun's overall visibility, w: degree of the sunset)
	};

	v2f vert(v3 v:POSITION)
	{
		v2 rs = _ScreenParams.xy; // screen resolution
		v2 sp = screen_point(v.xy, rs);

		m3 C = (m3)_CameraTransform; // camera transform
		v3 ro = _CameraTransform[3].xyz; // ray origin

		v1 ft = _CameraFovTan;
		v2 fsp; // sun flare screen position
		v1 sov; // sun's overall visibility
		v1 ssd; // degree of the sunset
		sun_visibility(ro, C, ft, rs.x / rs.y, sov, ssd, fsp);

		v1 oc = occlusion(fsp, _OcclusionRadius, rs.x / rs.y, _CameraDepthTexture, sampler_CameraDepthTexture);
		sov -= oc;
		sov = sat0(sov);

		v4 fc = float4(v.xy, 0, 1.0);

		v2f o;
		o.pos = fc;
		o.texcoord = TransformTriangleVertexToUV(v.xy);
#if UNITY_UV_STARTS_AT_TOP
		o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
		o.sun = v4(fsp, sov, ssd);

		return o;
	}
	
	float4 Frag(v2f i) : SV_Target
	{
		v2 fc = i.pos.xy; // frag coordinate
		v2 rs = _ScreenParams.xy; // screen resolution

		m3 C = (m3)_CameraTransform; // camera transform
		v2 sp = screen_point(fc.xy, rs);

		v2 fsp = i.sun.xy; // sun flare screen position
		v1 sov = i.sun.z;  // sun's overall visibility
		v1 ssd = i.sun.w;  // degree of the sunset

		float4 color = v4_0;
		color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
		color.rgb += sun_lens_flare(sp, fsp, sov, ssd);
		

		//return v4(sp.x, sp.y, 0.0, 1.0);
		//return v4(fc.x, fc.y, 0.0, 1.0);
		//return debug(ssd);

		return color;
	}

	ENDHLSL

	SubShader
	{
		Cull Off 
		ZWrite Off 
		ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex vert
				#pragma fragment Frag

			ENDHLSL
		}
	}
}