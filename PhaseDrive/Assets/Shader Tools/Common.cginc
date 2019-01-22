#ifndef COMMON
#define COMMON

// ----TOOLS----
#define iTime _Time
#define iResolution _ScreenParams
#define i1 int
#define v1 float
#define v2 float2
#define v3 float3
#define v4 float4
#define m3 float3x3
#define m4 float4x4
#define fract frac
#define mix lerp
#define v3_0 v3(0.0, 0.0, 0.0)
#define v3_1 v3(1.0, 1.0, 1.0)
#define v4_0 v4(0.0, 0.0, 0.0, 0.0)


static const float pi = 3.1415926535;
static const float degrees = pi / 180.0;
static const float inf = 1.0 / 1e-10;

//sat -> saturate
v1 sat(v1 v) { return clamp(v, 0., 1.); }
v2 sat(v2 v) { return clamp(v, 0., 1.); }
v3 sat(v3 v) { return clamp(v, 0., 1.); }
v4 sat(v4 v) { return clamp(v, 0., 1.); }
v1 sat0(v1 v) { return max(v, 0.); }
v2 sat0(v2 v) { return max(v, 0.); }
v3 sat0(v3 v) { return max(v, 0.); }
v4 sat0(v4 v) { return max(v, 0.); }
v1 sat1(v1 v) { return min(v, 1.); }
v2 sat1(v2 v) { return min(v, 1.); }
v3 sat1(v3 v) { return min(v, 1.); }
v4 sat1(v4 v) { return min(v, 1.); }

float sfloor(float v, float w) { return floor(v) + smoothstep(0.5 - w, 0.5 + w, fract(v)); }
float sbands(float v, float n, float w) { return sfloor(v * n, w) / n; }

float from11to01(float v) { return 0.5*(v + 1.0); }
v2  from11to01(v2 v) { return 0.5*(v + 1.0); }
float from11to(float v, float min, float max) { return (max - min)*from11to01(v) + min; }
float from01to11(float v) { return 2.*(v - 0.5); }
v2  from01to11(v2 v) { return 2.*(v - 0.5); }
float from01to(float v, float min, float max) { return (max - min)*v + min; }

float smin(float a, float b, float k)
{
	float h = clamp(0.5 + 0.5*(b - a) / k, 0.0, 1.0);
	return mix(b, a, h) - k * h*(1.0 - h);
}

float smax(float a, float b, float k)
{
	float h = clamp(0.5 + 0.5*(a - b) / k, 0.0, 1.0);
	return mix(b, a, h) + k * h*(1.0 - h);
}

v1 from_to(v1 from, v1 to) { return to - from; }
v2 from_to(v2 from, v2 to) { return to - from; }
v3 from_to(v3 from, v3 to) { return to - from; }

float  sqr(float v) { return v * v; }
float pow2(float v) { return v * v; }
float pow3(float v) { return v * pow2(v); }
float pow4(float v) { return pow2(pow2(v)); }
float pow5(float v) { return v * pow4(v); }
float pow6(float v) { return pow2(v)*pow4(v); }
float pow7(float v) { return v * pow6(v); }
float pow8(float v) { return pow2(pow4(v)); }
float pow9(float v) { return v * pow8(v); }
float pow10(float v) { return pow2(v)*pow8(v); }

float vsqr(v2 v) { return dot(v, v); }
float vsqr(v3 v) { return dot(v, v); }

float sstep(float min, float max, float width) { return smoothstep(min - width, min + width, max); }
float estep(float x, float k, float n) { return exp(-k * pow(x, n)); }


float pulse3(float x, float c, float w)
{
	x = abs(x - c);
	if (x > w) return 0.0;
	x /= w;
	return 1.0 - x * x*(3.0 - 2.0*x);
}

float pulse6(float x, float c, float w)
{
	return pow2(pulse3(c, w, x));
}

float almost_identity(float x, float m, float n)
{
	if (x > m) return x;

	float a = 2.0*n - m;
	float b = 2.0*m - 3.0*n;
	float t = x / m;

	return (a*t + b)*t*t + n;
}

float smoothout(float x, float m)
{
	float n = m / 2.;
	return (almost_identity(x, m, n) - n) / (1. - n);
}

v3 smoothout(v3 v, v3 m)
{
	return v3
	(
		smoothout(v.x, m.x),
		smoothout(v.y, m.y),
		smoothout(v.z, m.z)
	);
}

float gain(float x, float k)
{
	float a = 0.5*pow(2.0*((x < 0.5) ? x : 1.0 - x), k);
	return (x < 0.5) ? a : 1.0 - a;
}


v1 wdot
(
	in v3 n // normal
	, in v3 l // direction to the light
	, in v1 w // wrapping factor
)
{
	return sat0((dot(n, l) + w) / (1. + w));
}

v3 wdot
(
	in v3 n  // normal
	, in v3 l // direction to the light
	, in v3 w  // wrapping factor
)
{
	return v3
	(
		wdot(n, l, w.r),
		wdot(n, l, w.g),
		wdot(n, l, w.b)
	);
}

//----COLOR----
v3 rgb2hsl(v3 color) {
	v3 hsl; // init to 0 to avoid warnings ? (and reverse if + remove first part)

	float fmin = min(min(color.r, color.g), color.b); //Min. value of RGB
	float fmax = max(max(color.r, color.g), color.b); //Max. value of RGB
	float delta = fmax - fmin; //Delta RGB value

	hsl.z = (fmax + fmin) / 2.0; // Luminance

	if (delta == 0.0) //This is a gray, no chroma...
	{
		hsl.x = 0.0; // Hue
		hsl.y = 0.0; // Saturation
	}
	else //Chromatic data...
	{
		if (hsl.z < 0.5)
			hsl.y = delta / (fmax + fmin); // Saturation
		else
			hsl.y = delta / (2.0 - fmax - fmin); // Saturation

		float deltaR = (((fmax - color.r) / 6.0) + (delta / 2.0)) / delta;
		float deltaG = (((fmax - color.g) / 6.0) + (delta / 2.0)) / delta;
		float deltaB = (((fmax - color.b) / 6.0) + (delta / 2.0)) / delta;

		if (color.r == fmax)
			hsl.x = deltaB - deltaG; // Hue
		else if (color.g == fmax)
			hsl.x = (1.0 / 3.0) + deltaR - deltaB; // Hue
		else if (color.b == fmax)
			hsl.x = (2.0 / 3.0) + deltaG - deltaR; // Hue

		if (hsl.x < 0.0)
			hsl.x += 1.0; // Hue
		else if (hsl.x > 1.0)
			hsl.x -= 1.0; // Hue
	}

	return hsl;
}

v3 rgb2hsv(v3 c)
{
	v4 K = v4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	v4 p = mix(v4(c.bg, K.wz), v4(c.gb, K.xy), step(c.b, c.g));
	v4 q = mix(v4(p.xyw, c.r), v4(c.r, p.yzx), step(p.x, c.r));

	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return v3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

v3 hsv2rgb(v3 c)
{
	v4 K = v4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	v3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}


v3 dither(v2 sp, v1 t, v1 d)
{
	v1 v = dot(v2(131.0, 312.0), sp + t);
	v3 c = v3(v, v, v);
	c = fract(c / v3(103.0, 71.0, 97.0)) - v3(0.5, 0.5, 0.5);
	return (c / d) * 0.375;
}

//----DEBUG----

v4 debug(v1 v, v1 b)
{
	v1 sv = v * b;
	v1 f = fract(sv);
	v1 df = abs(fwidth(sv));

	v1 g = smoothstep(0.0, 2.0*df, f);

	v1 cv = clamp(v, -1., 1.);
	return g * v4
	(
		step(0.0, v)*cv*(v3(0.323, 0.915, 0.340))
		- step(v, 0.0)*cv*(v3(0.895, 0.311, 0.217))
		+ step(1.0, v)*(v - 1.) * v3(0.5, 0., 1.)
		+ step(v, -1.)*(v + 1.) * v3(0., 1., 0.5),
		1.0
	);
}

v4 debug(v1 v) { return debug(v, 1.); }

v4 debug(v2 v)
{
	return v4
	(
		(v.x*normalize(v3(0.323, 0.915, 0.340))
			+ v.y*normalize(v3(0.895, 0.311, 0.217)))* 0.5
		, 1.0
	);
}

v4 debug(v3 v)
{
	return v4(v, 1.0);
}

v4 isolines
(
	in v1 v // value
	, in v1 s // scale
)
{
	v1 sv = v * s;
	v1 f = fract(sv);
	v1 n = floor(sv) / s;
	v1 df = abs(fwidth(sv));

	v1 g = -1. + smoothstep(0.0, 2.0*df, f) + n;

	return v4(g, g, g, 1.0);
}

float graph(v2 p, float f, float w)
{
	return smoothstep(f - w, f, p.y) - smoothstep(f, f + w, p.y);
}

v3 graph_color(v2 sc, v3 c, float w)
{
	v3 result = v3_0;
	float h = graph(sc, rgb2hsv(c.xyz).x, w);
	float s = graph(sc, rgb2hsv(c.xyz).y, w);
	float l = graph(sc, rgb2hsv(c.xyz).z, w);
	result += v3(h, h, 0.);
	result += v3(s, 0., s);
	result += v3(0., l, l);

	float r = graph(sc, c.xyz.r, w);
	float g = graph(sc, c.xyz.g, w);
	float b = graph(sc, c.xyz.b, w);
	result += v3(r, 0., 0.);
	result += v3(0., g, 0.);
	result += v3(0., 0., b);

	return result;
}

//----PROJECTION----
v3 plane_project
(
	  v3 v  // vector
	, v3 pn // plane normal
)
{
	return from_to(dot(v, pn)*pn, v);
}

//----ROTATION----
m3 rotationQ(v3 axis, float rad)
{
	float hr = rad / 2.0;
	float  s = sin(hr);
	v4   q = v4(axis * s, cos(hr));
	v3  q2 = q.xyz + q.xyz;
	v3 qq2 = q.xyz * q2;
	v2  qx = q.xx  * q2.yz;
	float qy = q.y   * q2.z;
	v3  qw = q.w   * q2.xyz;

	return m3
	(
		1.0 - (qq2.y + qq2.z), qx.x - qw.z, qx.y + qw.y,
		qx.x + qw.z, 1.0 - (qq2.x + qq2.z), qy - qw.x,
		qx.y - qw.y, qy + qw.x, 1.0 - (qq2.x + qq2.y)
	);
}

m3 rotationX(float rad)
{
	float c = cos(rad);
	float s = sin(rad);
	return m3
	(
		1.0, 0.0, 0.0,
		0.0, c, s,
		0.0, -s, c
	);
}

m3 rotationY(float rad)
{
	float c = cos(rad);
	float s = sin(rad);
	return m3
	(
		c, 0.0, -s,
		0.0, 1.0, 0.0,
		s, 0.0, c
	);
}

m3 rotationZ(float rad)
{
	float c = cos(rad);
	float s = sin(rad);
	return m3
	(
		c, s, 0.0,
		-s, c, 0.0,
		0.0, 0.0, 1.0
	);
}

//----CAMERA----
v2 screen_point
(
	in v2 fc // frag coordinate
	, in v2  r // resolution 
)
{
	return (2.0*fc.xy - r.xy) / r.y;
}

v1 pixel_size(in v2 r)
{
	return 1. / r.y;
}

void polar
(
	in v2 sp // screen point
	, out v1  r // distance
	, out v1  a // angle
)
{
	a = atan2(sp.y, sp.x);
	r = length(sp);
}

//----RAYCAST----
v3 screen_to_ray
(
	  in v2 p  // point on the screen (-1, 1)
	, in m3 C  // camera transform
	, in v1 ft // FOV tangent
)
{
	v3 t = mul(C, v3(p, 1.0 / ft));
	return normalize(t);
}

// w component has 1.0 if ray is in front of the camera, -1.0 otherwise
v3 ray_to_screen
(
	in v3 v  // ray direction
	, in m3 C  // camera transform
	, in v1 ft // FOV tangent
)
{
	v3 p = mul(v, C);
	return v3(p.xy / (p.z * ft), sign(p.z));
}

void basis_from_angles
(
	   in v3 a //angles
	, out v3 f // forward direction
	, out v3 u // up direction
	, out v3 r // right direction
)
{
	static const v3 br = v3(1.0, 0.0, 0.0); // base right
	static const v3 bf = v3(0.0, 0.0, 1.0); // base forward
	static const v3 bu = v3(0.0, 1.0, 0.0); // base up

	m3 rt = rotationZ(a.z) * rotationY(a.y) * rotationX(a.x); // rotation

	f = mul(rt, bf);
	u = mul(rt, bu);
	r = mul(rt, br);
}

v3 ray_looking_at
(
	in v2 p, // point on the screen (-1, 1)
	in v3 o, // camera origin
	in v3 t, // camera target
	in v3 u, // up
	in v1 ft // FOV tangent
)
{
	v3 ww = normalize(from_to(o, t));
	v3 uu = normalize(cross(ww, u));
	v3 vv = normalize(cross(uu, ww));

	// create view ray
	return normalize(-p.x*uu + p.y*vv + ww / ft);
}

v1 angular_pixel_size
(
	in v2 s  // screen size
	, in v1 ft // FOV tangent
)
{
	return 2.0 * ft / s.y;
}

// returns half the distance between intersection points
float intersect_sphere
(
	in v3 ro // ray origin
	, in v3 rd // ray direction, normalized
	, in v3 sc // sphere center
	, in v1 sr // sphere radius
	, in v1 px // pixel size
	, out v1 t1 // t-value 1, signed distance to the first intersection point
	, out v1 t2 // t-value 2, signed distance to the second intersection point
	, out v3 p1 // point 1, 1st intersection point (closest in the positive direction)
	, out v3 p2 // point 2, 2nd intersection point (farthest in the positive direction)
)
{
	v3 co; // from sphere's center to ray's origin
	float b, c, h; // solving the quadratic equation

	co = from_to(sc, ro);
	b = dot(rd, co);
	c = dot(co, co) - sr * sr;
	h = b * b - c;

	if (h <= 0.0) // miss
	{
		return 0.0;
	}

	h = sqrt(h);

	t1 = -b - h;
	t2 = -b + h;

	p1 = ro + t1 * rd;
	p2 = ro + t2 * rd;

	return h;
}

//returns pixel coverage
float sphere_intersection_normal_aa
(
	in v3 ro // ray origin
	, in v3 rd // ray direction, normalized
	, in v3 sc // sphere center
	, in v1 sr // sphere radius
	, in v1 ps // pixel size
	, out v3 n  // normal vector of the sphere at the nearest intersection point
)
{
	v3 oc = ro - sc;
	v1 b = dot(oc, rd);
	v1 c = dot(oc, oc) - sr * sr;
	v1 h2 = b * b - c; // square of the distance between intercestion center and a point

	v1 d = sqrt(sat0(sr*sr - h2)) - sr;
	v1 d1 = -b - sqrt(sat0(h2)); // distance to the closest point

	if (d1 <= 0.0)
		return 0.0;

	v1 s = sat0(d / d1);
	if (s >= ps)     // too far from an intersection
		return 0.0;

	n = from_to(sc, ro + rd * d1) / sr;
	return 1.0 - sat0(s / ps); // pixel coverage
}

float intersect_sphere_aa
(
	in v3 ro // ray origin
	, in v3 rd // ray direction, normalized
	, in v3 sc // sphere center
	, in v1 sr // sphere radius
	, in v1 px // pixel size
)
{
	v3 _;
	return sphere_intersection_normal_aa(ro, rd, sc, sr, px, _);
}

float intersect_sphere_for_normals
(
	in v3 ro // ray origin
	, in v3 rd // ray direction, normalized
	, in v3 sc // sphere center
	, in v1 sr // sphere radius
	, in v1 ps // pixel size
	, out v3 n1 // normal vector of the sphere at the nearest intersection point 
	, out v3 n2 // normal vector of the sphere at the farthest intersection point 
)
{
	v3 oc = ro - sc;
	v1 b = dot(oc, rd);
	v1 c = dot(oc, oc) - sr * sr;
	v1 h2 = b * b - c; // square of the distance between intercestion center and a point

	v1 d = sqrt(sat0(sr*sr - h2)) - sr;
	v1 h = sqrt(sat0(h2));
	v1 d1 = -b - h; // distance to the closest point

	if (d1 <= 0.0)
		return 0.0;

	v1 s = sat0(d / d1);
	if (s >= ps)     // too far from an intersection
		return 0.0;

	v1 d2 = -b + h; // distance to the farthest point

	n1 = from_to(sc, ro + rd * d1) / sr;
	n2 = from_to(sc, ro + rd * d2) / sr;
	return 1.0 - sat0(s / ps); // pixel coverage
}

float intersect_billboard_circle
(
	in v3 ro // ray origin
	, in v3 rd // ray direction, normalized
	, in v3 c  // center
	, in v1 r  // radius
	, in v1 ts // transition size, size of the transition of the returned value
	, out v3 p  // intersection point
)
{
	//oc - ray origin to circle center
	//oi - ray origin to intersection
	//ci - circle center to intersection
	//o  - offset from the circle edge

	v3  oc = from_to(ro, c);
	v3  oi = rd * vsqr(oc) / dot(rd, oc);
	v3  ci = from_to(oc, oi);
	float ci_sqr = vsqr(ci);
	float r_sqr = sqr(r);
	if (ci_sqr > r_sqr)
		return 0.0;

	p = ro + oi;
	return 1. - smoothstep(1. - ts, 1.0, sqrt(ci_sqr / r_sqr));
}

float intersect_billboard_circle
(
	in v3 ray_direction,
	in v3 circle_direction,
	in v1 cosine_width
)
{
	v3 rd = ray_direction;
	v3 cd = circle_direction;
	float w = cosine_width;

	return sat((dot(rd, cd) + w - 1.) / w);
}

//----SHAPES 2D----
v1 circle
(
	in v2 sp // screen point
	, in v2 c  // center
	, in v1 r  // radius
	, in v1 ps // pixel size
)
{
	return sstep(vsqr(from_to(c, sp)), sqr(r), sqr(ps));
}

//----NOISE----
float mod289(v1 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
v4 mod289(v4 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
v4 perm(v4 x) { return mod289(((x * 34.0) + 1.0) * x); }

float noise(v3 p) {
	v3 a = floor(p);
	v3 d = p - a;
	d = d * d * (3.0 - 2.0 * d);

	v4 b = a.xxyy + v4(0.0, 1.0, 0.0, 1.0);
	v4 k1 = perm(b.xyxy);
	v4 k2 = perm(k1.xyxy + b.zzww);

	v4 c = k2 + a.zzzz;
	v4 k3 = perm(c);
	v4 k4 = perm(c + 1.0);

	v4 o1 = fract(k3 * (1.0 / 41.0));
	v4 o2 = fract(k4 * (1.0 / 41.0));

	v4 o3 = o2 * d.z + o1 * (1.0 - d.z);
	v2 o4 = o3.yw * d.x + o3.xz * (1.0 - d.x);

	return o4.y * d.y + o4.x * (1.0 - d.y);
}

float ridged_noise(in v3 p) { return abs(from01to11(noise(p))); }
float mixed_noise(in v3 p)
{
	v1 n = noise(p);
	v1 rn = abs(from01to11(n));
	return n * rn;
}

static const m3 fbm_octave_rotation =
m3
(
	 0.00,  0.80,  0.60,
	-0.80,  0.36, -0.48,
	-0.60, -0.48,  0.64
);

float fbm
(
	v3 p,
	i1 octaves,
	v1 lacunarity,
	v1 gain
)
{
	float
		total = 0.0,
		amplitude = gain,
		sum_amplitude = 0.0
		;

	for (int i = 0; i < octaves; i++)
	{
		total += noise(p) * amplitude;
		p = mul(fbm_octave_rotation, p * lacunarity);
		sum_amplitude += amplitude;
		amplitude *= gain;
	}

	return total / sum_amplitude;
}

float ridged_fbm
(
	v3 p,
	i1 octaves,
	v1 lacunarity,
	v1 gain
)
{
	float
		total = 0.0,
		amplitude = gain,
		sum_amplitude = 0.0
		;

	for (int i = 0; i < octaves; i++)
	{
		total += ridged_noise(p) * amplitude;
		p = mul(fbm_octave_rotation, p * lacunarity);
		sum_amplitude += amplitude;
		amplitude *= gain;
	}

	return total / sum_amplitude;
}
#endif