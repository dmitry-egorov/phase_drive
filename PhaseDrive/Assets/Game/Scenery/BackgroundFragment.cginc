#include "Assets/Shader Tools/Common.cginc"

//----PARAMETERS----
//#define FARSHOT
//#define SKIP_OPAQUE
//#define SKIP_TRANSPARENT
#define CLOUDS_ONLY

static const v3 _SunDirection = normalize(v3(0., 0, -1.0));
static const v3 _PlanetCenter = v3(0.0, 0.0, 0.0);
static const v1 _PlanetRadius = 1.0;

#ifdef FARSHOT

static const v1 _CameraDistance = _PlanetRadius * 2.5;
static const v1 _CameraFov = pi / 8.;
static const v1 _CameraStartingAngle = -1.5;
static const v1 _CameraRotationSpeed = pi * 0.001;
static const v1 _CameraTilt = pi / 3.5;

static const v3 _BackgroundColor = v3(0.0, 0.0, 0.0);

static const v3 _DiffuseMargin = 0.3*v3(0.06, 0.053, 0.05);
static const v3 _DiffuseFalloffStart = .2*v3(0.97, 0.98, 0.99);
static const v3 _DiffuseSmoothing = v3(0.15);
static const v1 _DiffuseBoost = -0.2;
static const v1 _FresnelIntensity = 0.;

static const v1 _SunAngularRadius = 0.00004;
static const v3 _SunColor = 1.1*v3(220.0, 213.0, 150.0) / 255.0;
static const v1 _SunIntensity = 5.;
static const v3 _SunGlowColor = 1.1*v3(180.0, 218.0, 150.0) / 255.0;

static const v1 _StarsDimmingBySun = 0.9;

static const v3 _OceanColor = 0.4*v3(0.14, 0.34, 0.88);
//static const v3  _OceanColor = 0.45*v3(0.447, 0.698, 0.180);
static const v1 _OceanSpecularIntensity = 5.8;
static const v1 _OceanShiness = 32.;

static const v1 _CloudsCoverage = .07;
static const v1 _CloudsRigidness = 1.5;
static const int  _CloudsResolution = 9;
static const int  _CloudsUnwarpedDetail = 4;
static const v1 _CloudsPolarBias = 1.30;
static const v1 _CloudsDetailFalloff = 0.6;
static const v1 _CloudsSharpness = 0.3;
static const v1 _CloudsRidgesSharpness = 0.2;
static const v1 _CloudsTextureIntesity = 1.5;
static const v1 _CloudsSpecularIntensity = 0.25;
static const v1 _CloudsShiness = 16.;
static const v1 _CloudsShadeSharpness = 0.7;
static const v1 _CloudsShadeSpecularSharpness = .7;
static const v1 _CloudsShadowPower = 1.2;
static const v1 _CloudsHeight = 0.003;
//static const v1 _CloudsMorphSpeed = 0.0004;
static const v1 _CloudsMorphSpeed = 0.0002;
static const v1 _CloudsDistanceDetailFalloff = 0.8;
static const v1 _CloudsDistanceDetailCutoff = 0.1;
static const v1 _CloudsScale = 2.;
static const v1 _CloudsSquosh = 1.2;
static const v1 _CloudsMorphOffset = 1.5;
static const v3 _CloudsBrightColor = v3(.94, 0.94, 0.96);
static const v3 _CloudsDarkColor = 0.9*v3(0.66, 0.72, 0.80);
static const v1 _CloudsPreWarp = 2.3;
static const v1 _CloudsWarp = 1.3;
static const v3 _CloudsShadeColor = v3(25, 24, 28) / 255.;
static const v3 _CloudsHighlightColor = 1.2*v3(1., 1., 1.);
static const v4 _CloudsShadowColor = v4(0.2 * v3(25, 24, 28) / 255., 0.6);
static const v1 _CloudsHighlightBoost = 0.4;
static const v1 _CloudsHighlightBoostThreashold = 0.6;

static const v1 _AtmosphereRadius = 0.08;
static const v1 _AtmosphereInnerFalloff = 2.7;
static const v1 _AtmosphereInnerSharpness = 0.6;
static const v1 _AtmosphereInnerSmoothness = 0.2;
static const v1 _AtmosphereInnerMargin = 0.1;
static const v1 _AtmosphereOuterSharpness = 1.4;
static const v1 _AtmosphereOuterSmoothness = 0.5;
static const v1 _AtmosphereOuterMargin = 0.1;
static const v1 _AtmosphereIntensity = 0.8;
static const v1 _AtmosphereZenithIntensity = 1.1;
static const v1 _AtmosphereSunsetIntensity = 0.5;
static const v1 _AtmosphereOuterPeakOffset = 0.6;
static const v1 _AtmosphereSunsetWidth = 0.3;
static const v1 _AtmosphereSunsetPeakWidth = 0.1;
static const v3 _AtmosphereInnerColor = v3(13., 30., 68.) / 255.;
static const v3 _AtmosphereOuterColor = 2.*v3(13., 23., 68.) / 255.;
static const v3 _AtmosphereSunsetColor = 1.5*v3(52., 160., 232.) / 255.;
static const v3 _AtmosphereSunsetPeakColor = v3(0.95, 0.25, 0.00);

#else //----CLOSE SHOT----

static const v1 _CameraDistance = _PlanetRadius * 1.06;
static const v1 _CameraFov = pi / 6.;
static const v1 _CameraStartingAngle = -1.;
static const v1 _CameraRotationSpeed = pi * 0.002;
static const v1 _CameraTilt = pi / 4.;

static const v3 _BackgroundColor = v3(0.0, 0.0, 0.0);

static const v3 _DiffuseMargin = 0.1 * v3(0.12, 0.110, 0.10);
static const v3 _DiffuseFalloffStart = .2*v3(0.94, 0.98, 0.99);
static const v3 _DiffuseSmoothing = 0.35*v3(0.4, 0.6, 0.85);
static const v1 _DiffuseBoost = -0.3;
static const v1 _FresnelIntensity = 0.;

static const v1 _SunAngularRadius = 0.0005;
static const v3 _SunColor = 1.1*v3(220.0, 213.0, 150.0) / 255.0;
static const v1 _SunIntensity = 5.;
static const v3 _SunGlowColor = 1.1*v3(180.0, 218.0, 150.0) / 255.0;

static const v1 _StarsDimmingBySun = 0.9;

static const v3 _OceanColor = 0.4*v3(0.14, 0.34, 0.88);
//static const v3  _OceanColor = 0.45*v3(0.447, 0.698, 0.180);
static const v1 _OceanSpecularIntensity = 5.8;
static const v1 _OceanShiness = 32.;

static const v1 _CloudsCoverage = .01;
static const v1 _CloudsRigidness = 2.3;
static const i1 _CloudsResolution = 11;
static const i1 _CloudsUnwarpedDetail = 3;
static const v1 _CloudsPolarBias = 1.30;
static const v1 _CloudsDetailFalloff = 0.66;
static const v1 _CloudsSharpness = 0.125;
static const v1 _CloudsTextureIntesity = .8;
static const v1 _CloudsRidgesSharpness = 0.2;
static const v1 _CloudsSpecularIntensity = .2;
static const v1 _CloudsShiness = 16.;
static const v1 _CloudsShadeSharpness = 0.2;
static const v1 _CloudsShadeSpecularSharpness = 0.05;
static const v1 _CloudsShadowPower = 1.2;
static const v1 _CloudsHeight = 0.0013;
static const v1 _CloudsDistanceDetailFalloff = 0.5;
static const v1 _CloudsDistanceDetailCutoff = 0.1;
static const v1 _CloudsScale = 10.;
static const v1 _CloudsSquosh = 1.2;
static const v1 _CloudsPreWarp = 2.3;
static const v1 _CloudsWarp = 1.8;
static const v1 _CloudsHighlightBoost = 0.6;
static const v1 _CloudsHighlightBoostThreashold = 0.2;
static const v3 _CloudsBrightColor = v3(.94, 0.94, 0.96);
static const v3 _CloudsDarkColor = 1.0*v3(0.66, 0.72, 0.80);
static const v3 _CloudsShadeColor = v3(25, 24, 28) / 255.;
static const v3 _CloudsHighlightColor = 1.2*v3(1., 1., 1.);
static const v4 _CloudsShadowColor = v4(0.2 * v3(25, 24, 28) / 255., 0.6);
static const v1 _CloudsMorphOffset = 1.5;
static const v1 _CloudsMorphSpeed = 0.0004;

static const v1 _AtmosphereRadius = 0.05;
static const v1 _AtmosphereInnerFalloff = 2.8;
static const v1 _AtmosphereInnerSharpness = 0.6;
static const v1 _AtmosphereInnerSmoothness = 0.05;
static const v1 _AtmosphereInnerMargin = 0.06;
static const v1 _AtmosphereOuterSharpness = 0.9;
static const v1 _AtmosphereOuterSmoothness = 0.4;
static const v1 _AtmosphereOuterMargin = 0.2;
static const v1 _AtmosphereIntensity = 0.35;
static const v1 _AtmosphereZenithIntensity = 1.5;
static const v1 _AtmosphereSunsetIntensity = 0.2;
static const v1 _AtmosphereOuterPeakOffset = 0.7;
static const v1 _AtmosphereSunsetWidth = 0.8;
static const v1 _AtmosphereSunsetPeakWidth = 0.3;
static const v3 _AtmosphereInnerColor = v3(13., 30., 68.) / 255.;
static const v3 _AtmosphereOuterColor = 2.*v3(13., 23., 68.) / 255.;
static const v3 _AtmosphereSunsetColor = 1.5*v3(52., 160., 232.) / 255.;
static const v3 _AtmosphereSunsetPeakColor = v3(0.95, 0.25, 0.00);
#endif

static const v1 _CameraFovTan = tan(_CameraFov);

//----TIME----
v1 time()
{
	return iTime;
}

//----STARS----
v4 stars
(
	  in v3 rd // ray direction
	, in v1 ps // pixel size
	, in v1 sv // sun visibility
)
{
	v1 sd = _StarsDimmingBySun;

	//TODO: parametrize!!!
	v3 sp = (iResolution.y / 6.)*rd; // sampling point
	v1 shape = fbm(sp, 3, 2.01, 0.55);
	v3 color = hsv2rgb(v3(noise(2.*sp), 3.*(1. - shape), 0.8));

	v1 v = (1. - sd * sqrt(sv)) * sstep(0.86, shape, ps); // visibility

	return v4(color, v);
}


// ----PLANET----
v1 clouds_shape
(
	in v3 rd // ray direction
	, in v3  n // normal
	, in v3 sp // sampling point
	, in v1  w // warp
	, in v1  r // raise
)
{
	v1 l = 2.01; // lacunarity

	int uo = _CloudsUnwarpedDetail; // unwarped octaves
	int wo = _CloudsResolution - uo; // warped octaves
	v1  wof = float(wo);

	v1 df = _CloudsDetailFalloff;
	v1 ns = fbm(sp + w, wo, l, df); // noise

	v3 rsp = sp * pow(l, wof); // rescaled point
	v1 rsg = pow(df, wof); // rescaled gain

	v1 ddf = _CloudsDistanceDetailFalloff;
	v1 ddc = _CloudsDistanceDetailCutoff;
	v1 d = pow(wdot(n, -rd, -ddc), ddf); // detail
	if (d > 0.0)
	{
		ns += rsg * fbm(rsp, uo, l, d * df);
	}

	v1 bnns = sbands(from01to11(ns), 10., _CloudsSharpness);// banded noise of the shape

	v1 rg = 20. * _CloudsRigidness; // rigidness
	return rg * 0.23*bnns + _CloudsCoverage + _CloudsPolarBias * r;
}

v1 clouds_texture
(
	in v3 rd // ray direction
	, in v3  n // normal
	, in v3 sp // sampling point
	, in v1  w // warp
	, in v1  r // raise
)
{
	v1  l = 2.01; // lacunarity
	v3 wsp = sp + w; // warped sampling point
	v1 mn = 0.0; // main noise 
	mn += 0.5*pow(ridged_noise(wsp*0.5), 1.1);
	mn += pow(ridged_noise(wsp), 1.1);
	mn += 0.5*pow(ridged_noise(wsp * 2.0 + v3_1), 1.1);
	mn += 0.25*noise(wsp * 4.0 + 3.*v3_1);
	mn += 0.13*noise(wsp * 8.0 + 7.*v3_1);
	mn -= 0.12*noise(wsp * 16.0 + 023.*v3_1);
	mn += 0.06*noise(wsp * 32.0 + 111.*v3_1);

#ifdef FARSHOT
	mn += 0.08*noise(wsp * 64.0 + 283.*v3_1);
	mn += 0.04*noise(wsp * 128.0 + 433.*v3_1);
	mn = sbands(mn, 4., 0.4);
#else

	v3 mfsp = (sp + 0.3*w) * 64.0 + 83.*v3_1; // mid frequency warped sampling point
	v1 mfn = 0.0;
	mfn += noise(mfsp);
	mfn += 0.4*noise(mfsp * 2. + 523.*v3_1);
	mfn *= 0.06;

	v1 hfn = 0.0; // high frequency noise

	v1 f = pow(wdot(-rd, n, 0.2), 0.6);

	if (f > 0.0)
	{
		v3 hfsp = (sp + 0.2*w) * 256.0 * 1.1 + 203.*v3_1; // high frequency warped sampling point
		hfn += noise(hfsp);
		hfn += 0.30*noise(hfsp * 2. + 1022.*v3_1);
		hfn += 0.15*noise(hfsp * 4. + 1911.*v3_1);
		hfn = pow(hfn, 2.);
		hfn *= f * 0.005;
	}

	mn += 0.6*mfn + -2.0*hfn;
	mn = sbands(mn, 4., 0.18);
	mn += 1.*mfn + 2.*hfn;
#endif

	mn *= _CloudsTextureIntesity;
	return mn;
}

v3 sampling_point
(
	in v3 n // normal
	, in v1 t // time
)
{
	v1 ddf = _CloudsDistanceDetailFalloff;
	v1 ddc = _CloudsDistanceDetailCutoff;

	v1 rg = 20. * _CloudsRigidness; // rigidness

	v3 sp = n;
	sp *= v3(1.0, _CloudsSquosh, 1.0) *_CloudsScale;
	sp += _CloudsMorphOffset + t * _CloudsMorphSpeed;

	return sp;
}

v1 warp
(
	in v3 sp // sampling point
)
{
#if 0
	v1 ww = _CloudsPreWarp * fbm(sp, 4, 2.01, 0.6); // warp's warp offset
	return _CloudsWarp * fbm(sp + ww, 4, 2.01, 0.4); // warp offset
#elif 1
	v1 pw = 0.0;
	pw += 0.4 * ridged_noise(sp*1.5);
	pw += 0.1 * noise(sp*4.);
	pw *= _CloudsPreWarp;
	v1 w = 0.0;
	w += 0.4*noise(sp*1.5 + pw);
	w += 0.15*noise(sp*4. + pw);
	w += 0.1*noise(sp*8. + pw);

	return _CloudsWarp * w; // warp offset
#else
	return 0.;
#endif
}

//returns color and specular reduction
v4 shade
(
	in v3  rd // ray direction
	, in v3   n // normal
	, in v3  sd // sun direction
	, in v3 shd // shadow direction
	, in v1   t // time
	, in v1   r // raise
	, in v1  tx // texture
)
{
	v1 eps = 0.0001;
	v3 son = normalize(n + eps * normalize(shd)); // shade offset sample point
	v3 osp = sampling_point(son, t);
	v1  sw = warp(osp);
	v1 shtx = clouds_texture(rd, son, osp, sw, r); // shader texture

	v1 shr = 0.0015*_CloudsShadeSharpness;
	v1 sshr = 0.0015*_CloudsShadeSpecularSharpness;

	v1 d = from_to(shtx, tx) / eps;
	d = -d;//invert high low;

	v1 hbi = _CloudsHighlightBoost;
	v1 hbt = _CloudsHighlightBoostThreashold;
	v1 hb = 1.; // highlight boost
	hb += hbi * (0.8 - smoothstep(0., hbt, wdot(sd, n, 0.1)));

	v3 shdc = v3_0;
	shdc += 0.15*step(d, 0.0)*(1. - _CloudsShadeColor)*d; // shade
	shdc += step(0.0, d)*_CloudsHighlightColor*hb*d; // highlight
	shdc *= shr;

	v1 shds = d * sshr;
	return v4(shdc, shds);
}

void clouds
(
	in v3  rd, // ray direction
	in v3   n, // normal 
	in v1   t, // time
	out v4   c,
	out v2 spc,
	out v4  sh
)
{
	v1  r = smoothstep(0.85, 1.0, abs(sbands(n.y, 2., 0.4))); // raise
	v3 sp = sampling_point(n, t); // sampling point
	v1  w = warp(sp); // warp

	v3 sd = _SunDirection;
	v3 shd = plane_project(sd, n); // shadow direction

	v1 shp; // shape
	sh = v4_0;
#ifdef CLOUDS_ONLY 
	shp = 1.0;
#else
	shp = clouds_shape(rd, n, sp, w, r);
	if (shp < 1.0)
	{
		v3 son = normalize(n + _CloudsHeight * shd); // shadow offset point
		v3 osp = sampling_point(son, t);
		v1  sw = warp(osp);
		v1  ss = clouds_shape(rd, son, osp, sw, r);
		shd = v4(_CloudsShadowColor.xyz, _CloudsShadowColor.w*pow(sat(ss), _CloudsShadowPower));
	}
#endif

	//project the sun direction to the surface

	spc = v2(_CloudsSpecularIntensity, _CloudsShiness);
	c = v4_0;
	if (shp > 0.0)
	{
		v1 tx = clouds_texture(rd, n, sp, w, r); // texture
		v3 cl = mix(_CloudsDarkColor, _CloudsBrightColor, tx);

		//shade
		v4 shdc = shade(rd, n, sd, shd, t, r, tx);
		cl += shdc.rgb;
		spc.x += shdc.w;
		c = v4(cl, sat(shp));
	}
}

void surface
(
	in v3 p,
	out v3 c,
	out v2 s
)
{
	c = _OceanColor;
	s = v2(_OceanSpecularIntensity, _OceanShiness);
}

v3 light
(
	  in v3 n  // normal
	, in v3 rd // ray direction
	, in v3 dc // diffuse color
	, in v2 sp // specular intensity and shiness
)
{

	v3 sd = _SunDirection;
	v1 si = sp.x; // specular intensity
	v1 ss = sp.y; // specular shiness

	// diffuse
	v3 dm = _DiffuseMargin; // light margin
	v3 df = _DiffuseFalloffStart;
	v3 d = wdot(n, sd, dm);
	d = smoothstep(v3_0, df, d);
	d = smoothout(d, _DiffuseSmoothing);
	d *= 1. + _DiffuseBoost;

	// specular
	v3 sc = _SunColor;
	v1 fi = _FresnelIntensity;

	v3 h = normalize(from_to(rd, sd)); // half vector
	v1 fr = fi * pow(sat1(1. + dot(h, rd)), 5.0); // fresnel
	v3 s = dc * sc*si*pow(sat0(dot(n, h)), ss);

	return (dc + s + fr) * d;
}

v4 planet
(
	in v3 ro // ray origin
	, in v3 rd // ray direction
	, in v1 t  // time
	, in v1 ps // pixel size
)
{
	v3 pc = _PlanetCenter;
	v1 pr = _PlanetRadius;

	v3 inn; // intersection normal
	v1 hit = sphere_intersection_normal_aa(ro, rd, pc, pr, ps, inn);
	if (hit <= 0.0)
	{
		return v4_0;
	}

	v4 cc;  // clouds color
	v2 cs;  // clouds specular
	v4 csh; // clouds shadow
	clouds(rd, inn, t, cc, cs, csh);

	v3 fd; // final diffuse
	v2 fs; // final specular

#ifdef CLOUDS_ONLY
	fd = cc.xyz;
	fs = cs;
#else
	v3 sc; // surface color
	v2 ss; // surface specular
	surface(inn, sc, ss);

	fd = mix(sc, csh.xyz, csh.w);
	fd = mix(fd, cc.xyz, cc.w);
	fs = mix(ss, v2(0.), csh.w);
	fs = mix(fs, cs, cc.w);
#endif

	v3 c; // final color
	c = light(inn, rd, fd, fs);

	return v4(c, hit);
}

// ----ATMOSPHERE----
v1 outer_atmosphere_density
(
	in v1  h // height
	, in v1 po // peak offset
)
{
	return 0.1 * pow2(((1. + po) / (h + po) - 1.));
}

v3 outer_atmosphere_sun_glow
(
	in v3 rd // ray direction
	, in v1  d // density
	, in v3 sd // sun direction
	, in v3 sc // sun color
)
{
	//TODO!!!!! Extract parameters
	return 20.0*sc*smoothstep(0.995, 1.0, pow(sat0(dot(rd, sd)), pow(d, 1.)));
}

v3 inner_atmosphere_color
(
	in v3 rd,  // ray direction
	in v3 inn, // sphere intersection normal
	in v3 col, // atmosphere color
	in v1 f    // falloff
)
{
	v1 d = 0.97 + dot(rd, inn);// angle of viewing
	v1 x = f * (1. - pow2(d));
	return sat(pow(col, x*v3_1));
}

v1 inner_atmosphere_visibility
(
	v3 n   // sphere intersection normal
	, v3 sd  // sun direction
	, v1 sh  // sharpness
	, v1 m   // margin
	, v1 sm  // smoothness
)
{
	v1 d = wdot(n, sd, m);
	d = smoothstep(0., sh, d);
	return smoothout(d, sm);
}

v1 outer_atmosphere_visibility
(
	v3 fn // furthest sphere intersection normal
	, v3 sd // sun direction
	, v1 sh // sharpness
	, v1  m // margin
	, v1 sm // smoothness

)
{
	v1 d = wdot(fn, sd, m);
	d = sat0(d);
	return smoothout(pow(d, sh), sm);
}

v1 atmosphere_zenith
(
	in v3 rd  // ray direction
	, in v3 sd  // sun direction
	, in v1 zi  // zenith intensity
)
{
	v1 crs = dot(rd, sd); // cosine between the view ray and the sun

	return zi * pulse3(crs, -1., 1.4); // zenith
}

v3 atmosphere_sunset
(
	in v3 rd   // ray direction
	, in v3 fn   // furthest intersection normal
	, in v3 sd   // sun direction
	, in v1 ssi  // sunset intensity
	, in v1 ssw  // sunset width
	, in v1 sspw // sunset peak width
	, in v3 ssc  // sunset color
	, in v3 sspc // sunset peak color

)
{
	v1 crs = dot(rd, sd); // cosine between the view ray and the sun
	v1 csrs = dot(normalize(rd - 0.4 * fn), sd); // cosine between the offset view ray and the sun

	v1 l = 0.0;
	l += 1.*pulse3(csrs, 1., ssw); // sunset
	l += 2.*pulse3(crs, 1., sspw); // direct sunset

	v3 c = mix(ssc, sspc, pow2(crs));
	return ssi * c*l;
}

v1 outer_atmosphere_height
(
	in v3 ro // ray origin
	, in v3 rd // ray direction
	, in v3 pc // planet center
	, in v1 pr // planet radius
	, in v1 ar // atmosphere radius
)
{
	v3 oc = from_to(ro, pc);
	v1 cpsi = dot(rd, normalize(oc));
	if (cpsi <= 0.)
		return 1.0;
	v1 tpsi = sqrt(1. - sqr(cpsi)) / cpsi;
	v1 a = sqrt(dot(oc, oc) - pr * pr); //distance to surface
	v1 h = a * (a*tpsi - pr) / (a + pr * tpsi); // absolute height above surface
	return h / (ar - pr);

}

v3 atmosphere
(
	in v3 ro // ray origin
	, in v3 rd // ray direction
	, in v1 ps // pixel size
)
{
	v1 oh; // normalized height of the outer atmosphere
	v1 od; // density of the outer atmosphere
	v3 ci; // inner color
	v1 vi; // inner visibility
	v3 co; // outer color
	v1 vo; // outer visibility
	v1  z; // zenith
	v3 ss; // sunset
	v3 sg; // sun glow
	v3  c; // resulting atmosphere color

	v3  bic = _AtmosphereInnerColor;
	v3  boc = _AtmosphereOuterColor;
	v1  ssi = 50.*_AtmosphereSunsetIntensity;
	v3  ssc = _AtmosphereSunsetColor;
	v3 sspc = _AtmosphereSunsetPeakColor;
	v1  ssw = _AtmosphereSunsetWidth;
	v1 sspw = _AtmosphereSunsetPeakWidth;
	v1   af = _AtmosphereInnerFalloff;
	v1   ai = _AtmosphereIntensity;
	v3   sd = _SunDirection;
	v3   sc = _SunColor;
	v3  sgc = _SunGlowColor;
	v1  shi = _AtmosphereInnerSharpness;
	v1  smi = _AtmosphereInnerSmoothness;
	v1   mi = _AtmosphereInnerMargin;
	v1  sho = _AtmosphereOuterSharpness;
	v1  smo = _AtmosphereOuterSmoothness;
	v1   mo = _AtmosphereOuterMargin;
	v1   zi = _AtmosphereZenithIntensity;
	v1   po = 0.1*_AtmosphereOuterPeakOffset;
	v3   pc = _PlanetCenter;
	v1   pr = _PlanetRadius;
	v1   ar = pr + _AtmosphereRadius;

	v3 nn; // atmosphere nearest intersection normal
	v3 fn; // atmosphere furthest intersection normal
	v1 ah; // atmosphere hit sin of the angle toward hit center
	ah = intersect_sphere_for_normals(ro, rd, pc, ar, ps, nn, fn);
	if (ah <= 0.0)
	{
		return v3_0;
	}

	v3 pn; // planet intersection normal
	v1 ph = sphere_intersection_normal_aa(ro, rd, pc, pr, ps, pn);

	vi = inner_atmosphere_visibility(pn, sd, shi, mi, smi);
	oh = outer_atmosphere_height(ro, rd, pc, pr, ar);
	od = outer_atmosphere_density(oh, po);
	co = boc * od;
	sg = outer_atmosphere_sun_glow(rd, od, sd, sgc);
	vo = outer_atmosphere_visibility(fn, sd, sho, mo, smo);
	ss = atmosphere_sunset(rd, fn, sd, ssi, ssw, sspw, ssc, sspc);
	z = atmosphere_zenith(rd, sd, zi);

	ci = inner_atmosphere_color(rd, pn, bic, af);
	c = ai * (ss + sg + z + 1.0)*mix(co*vo, ci*vi, ph);

	return mix(v3_0, (c), ah);
}

//----SUN----
v3 suns_horizon
(
	in v3 ro // ray origin
)
{
	v3 sd = _SunDirection;
	v3 pc = _PlanetCenter;
	v1  r = _PlanetRadius;

	v3   c = from_to(ro, pc); // distance vector to planet center
	v1  cd = length(c); // distance to the center
	v3  cn = c / cd; // direction to the center
	v3 sdp = normalize(plane_project(sd, c / cd)); // creating an ortonormal basis for the plane formed by c and sd

	v1 r2 = sqr(r);  // squared radius
	v1 rc = -r2 / cd; // c component of the rh
	v1 rs = sqrt(r2 - sqr(rc)); // sdp component of the rh

	v3 rh = rc * cn + rs * sdp; // radius vector to the horizon
	v3 h = c + rh; // vector to the horizen

	return normalize(h);
}

void sun_visibility
(
	  in v3 ro  // ray origin
	, in m3 C   // camera
	, in v1 ft  // FOV tangent
	, in v1 ar  // aspect ratio (x / y)
	, out v1 ov  // overall visibility
	, out v1 ssd // degree of the sunset
	, out v2 fsp // flare screen position
)
{
	v3 sd = _SunDirection;
	v3 pc = _PlanetCenter;
	v1 pr = _PlanetRadius;
	v1 atr = pr + _AtmosphereRadius;
	v1 ho = 0.13; // extra height (TODO: calculate from sun size)

	ov = 0.0;
	ssd = 0.0;
	fsp = v2_0;

	v1  h = outer_atmosphere_height(ro, sd, pc, pr, atr);
	v1 eh = sat((h + ho) / (1. + ho)); // extended height
	if (eh <= 0.)
		return;

	v3  sfd = h > 0.0 ? _SunDirection : suns_horizon(ro); // sun flare direction
	v3 rfsp = ray_to_screen(sfd, C, ft); // sun flare screen position

	v1 sm = 0.2; // screen visibility margin
	v1 sv = step(0.0, rfsp.z) * sat(sstep(abs(rfsp.x), ar + sm, sm) * sstep(abs(rfsp.y), 1.0 + sm, sm)); // screen visibility
	if (sv <= 0.)
		return;

	ssd = eh; // degree of sunset
	ov = sv * pow(eh, 0.7);
	fsp = rfsp.xy;
}

v4 sun
(
	in v3 rd // ray direction
)
{
	v3 d = _SunDirection;
	v1 r = _SunAngularRadius;
	v1 pixel_area = intersect_billboard_circle(rd, d, r);
	if (pixel_area <= 0.0)
	{
		return v4_0;
	}

	v1 i = 10.*_SunIntensity*sat(pow8(pixel_area));//intensity
	return v4(_SunColor * i, sat(i));
}

//----LENS FLARE----
v1 fins
(
	in v2 sp // screen point
	, in v1 rt // rotation
	, in v1 sc // scale
	, in v1  n // number of fins
	, in v1  f // falloff
	, in v1  l // length
	, in v1 rn // range
)
{
	v1 r, a;
	polar(sp, r, a);
	a += rt;
	r /= sc;

	v1 rs = abs(2.*fract(n*a / (2.*pi)) - 1.);
	rs = rn * rs;
	rs = pow(rs, f);
	rs = rs / pow(r, l);
	rs = sstep(0.07, rs, 0.0175) * rs;

	return rs;
}

v1 halo
(
	in v2  o // offset
	, in v1 rt // rotation
	, in v1 sc // scale
)
{
	v1 r, a;
	polar(o, r, a);
	a += rt;
	r /= sc;

	v1  s = 1.;  // size
	v1  g = 2.;   // glow
	v1  i = 0.02; // intensity
	v1 rg = 0.34; // rays glow
	v1 rn = 5.2;  // number of rays 
	v1 ro = 8.1;  // rays offset

	return (i / pow(r, g)) * (s + rg * abs(cos(rn*a + sin(ro*a))));
}

v1 glow
(
	in v2  o // offset
	, in v1 sc // scale
)
{
	v1 r, a;
	polar(o, r, a);
	r /= sc;

	v1  f = 1.3; // fallof
	v1 rn = 0.6; // range

	return exp(-rn * pow(r, f));
}

v3 lens_flare
(
	in v2  sp // screen point
	, in v2  cn // center
	, in v1  rt // rotation
	, in v1  sc // scale
	, in v1  fn // number of fins
	, in v1  fi // fins intensity
	, in v1  ff // fins falloff
	, in v1  fl // fins length
	, in v1  fr // fins range
	, in v1  fa // fins abberation
	, in v1 cfb // central fin boost
	, in v1 efb // extra fins boost
	, in v3  mc // main color
	, in v3 cs1 // first color shift
	, in v3 cs2 // seond color shift
)
{
	v2  o = from_to(cn, sp);// offset

	v3 c = v3_0;
	c += mc * halo(o, rt, sc);
	c += fi * cfb*fins(o, rt, sc, fn, ff, fl, fr);
	c += fi * cs1*efb*fins(o + v2(+fa, +fa), rt, sc, fn, ff, fl, fr);
	c += fi * cs2*efb*fins(o + v2(-fa, -fa), rt, sc, fn, ff, fl, fr);
	c += fi * cs1*efb*fins(o + v2(+fa, -fa), rt, sc, fn, ff, fl, fr);
	c += fi * cs2*efb*fins(o + v2(-fa, +fa), rt, sc, fn, ff, fl, fr);
	c += 0.1*mc;
	c *= glow(o, sc);

	return c;
}

v3 sun_lens_flare
(
	in v2 sp // screen point
	, in v1 ov  // overall visibility
	, in v1 ssd // degree of the sunset
	, in v2 fsp // flare screen position
)
{
	v3   mc = v3(0.856, 0.919, 1.000); // main color
	v3   rc = v3(0.900, 0.508, 0.342); // red shift
	v3   bc = v3(0.135, 0.305, 0.940); // blue shift
	v1   fn = 10.;
	v1  mfi = 1.3;   // main fins intensity
	v1  mff = 4.3;   // main fins falloff
	v1  mfl = 0.4;   // main length
	v1  mfr = 0.65;  // main range
	v1 mcfb = 2.5;   // main central fin boost
	v1  mfa = 0.012; // main abberation
	v1  sfi = 0.3;   // sunset fins intensity
	v1  sff = 8.;    // sunset fins falloff
	v1  sfl = 1.;    // sunset length
	v1  sfr = 0.55;  // sunset range
	v1 scfb = 1.;    // sunset central fin boost
	v1  sfa = 0.003; // sunset abberation
	v3  ssc = v3(0.856, 0.5, 0.100); // sunset color
	v3 ssrc = mix(rc, ssc, 0.3);
	v3 ssbc = mix(bc, ssc, 0.3);

	ssd = gain(pow(ssd, 0.5), 128.);

	v3   c = mix(ssc, mc, ssd);
	v3 sc1 = mix(ssrc, rc, ssd);
	v3 sc2 = mix(ssbc, bc, ssd);
	v1  fi = mix(sfi, mfi, ssd);
	v1  ff = mix(sff, mff, 0.5*(ssd + ov));
	v1  fl = mix(sfl, mfl, 0.5*(ssd + ov));
	v1  fr = mix(sfr, mfr, 0.5*(ssd + ov));
	v1 cfb = mix(scfb, mcfb, 0.5*(ssd + ov));;
	v1  fa = mix(sfa, mfa, ssd);
	v1  rt = pi / 3.; // rotation
	v1  sc = 0.6; // scale
	v1 efb = 1.2;

	return ov * lens_flare(sp, fsp, rt, sc, fn, fi, ff, fl, fr, fa, cfb, efb, c, sc1, sc2);
}

// ----MAIN----
v3 opaque_objects
(
	in v3 ro // ray origin
	, in v3 rd // ray direction
	, in v1 t  // time
	, in v1 ps // pixel size
	, in v1 sv // sun visibility
)
{
	v3 final_color = _BackgroundColor;

	v4 pc = planet(ro, rd, t, ps);
	if (pc.w >= 1.)
	{
		return pc.xyz;
	}

	v4 sc = sun(rd);
	if (sc.w < 1.0)
	{
		v4 stc = stars(rd, ps, sv);
		final_color = mix(final_color, stc.xyz, stc.w);
	}

	if (sc.w > 0.0)
	{
		final_color = mix(final_color, sc.xyz, sc.w);
	}

	if (pc.w > 0.0)
	{
		final_color = mix(final_color, pc.xyz, pc.w);
	}

	return final_color;
}

v3 transparent_objects
(
	in v3 ro // ray origin
	, in v3 rd // ray direction
	, in v1 ps // pixel size
	, in v2 sp // screen point
	, in v1 sov // sun overall visibility
	, in v1 ssd // degree of the sunset
	, in v2 fsp // flare screen position
)
{
	v3 c = v3_0;
	c += atmosphere(ro, rd, ps);
	//c += sun_lens_flare(sp, sov, ssd, fsp);
	return c;
}

v4 background
(
	  in v2  fc // frag coordinate
	, in v2  sp // screen position
	, in v3  ro // ray origin
	, in v3  rd // ray direction
	, in v1  ps // pixel size
	, in v1   t // time
    , in v1 sov // sun's overall visibility
    , in v1 ssd // degree of the sunset
    , in v2 fsp // sun flare screen position
)
{
	v3 c = v3_0; // final color
#ifndef SKIP_OPAQUE
	c += opaque_objects(ro, rd, t, ps, sov);
#endif
#ifndef SKIP_TRANSPARENT
	c += transparent_objects(ro, rd, ps, sp, sov, ssd, fsp);
#endif

	//c = pow(c, (1. / 2.2)*v3_1);
	//c += dither(fc, t, 100.);
	return v4(c, 1.0);
}