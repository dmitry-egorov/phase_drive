Shader "Hidden/Custom/Flat"
{
	HLSLINCLUDE

	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

	float _Blend;

	float4 Frag(VaryingsDefault i) : SV_Target
	{
		float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
		//float d = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo));
		float d = (SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo));

		
		return float4(c.w.xxx, 1.0);
		//d = 1.0 - d * 0.05;

		//c.rgb = lerp(c.rgb, float3(d, d, d), _Blend.xxx);
		if (d == 0.0)
			return float4(_Blend.xxx, 1.0);

		return c;
		//return float4(0.0, 0.0, 0.0, 1.0);
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

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}