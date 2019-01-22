// polynomial smooth min (k = 0.1);
float smoothmin( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}

float smoothmax( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(a-b)/k, 0.0, 1.0 );
    return mix( b, a, h ) + k*h*(1.0-h);
}

float saturate(float v) { return clamp(v, 0., 1.); }
vec2 saturate(vec2 v) { return clamp(v, 0., 1.); }
vec3 saturate(vec3 v) { return clamp(v, 0., 1.); }
vec4 saturate(vec4 v) { return clamp(v, 0., 1.); }
float smoothfloor(float v, float w) { return floor(v) + smoothstep(0.5 - w, 0.5 + w, fract(v)); }
float smoothbands(float v, float n, float w) { return smoothfloor(v * n, w) / n; }
float from_11_to_01(float v) { return 0.5*(v + 1.0); }
vec2  from_11_to_01(vec2 v) { return 0.5*(v + 1.0); }
float from_11_to(float v, float min, float max) { return (max - min)*from_11_to_01(v) + min; }
float from_01_to_11(float v) { return 2.*(v - 0.5); }
vec2  from_01_to_11(vec2 v) { return 2.*(v - 0.5); }
float from_01_to(float v, float min, float max) { return (max - min)*v + min; }

#if 1
float noise( in vec3 pos )
{
  #if 0
    vec3 p = floor(pos);
    vec3 f = fract(pos);
	f = f*f*(3.0-2.0*f);
	vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;
	vec2 rg = textureLod( iChannel0, (uv+0.5)/256.0, 0.0).yx;
	return mix( rg.x, rg.y, f.z );
  #else
    ivec3 p = ivec3(floor(pos));
    vec3 f = fract(pos);
	f = f*f*(3.0-2.0*f);
	ivec2 uv = p.xy + ivec2(37,17)*p.z;
	vec2 rgA = texelFetch( iChannel0, (uv+ivec2(0,0))&255, 0 ).yx;
    vec2 rgB = texelFetch( iChannel0, (uv+ivec2(1,0))&255, 0 ).yx;
    vec2 rgC = texelFetch( iChannel0, (uv+ivec2(0,1))&255, 0 ).yx;
    vec2 rgD = texelFetch( iChannel0, (uv+ivec2(1,1))&255, 0 ).yx;
    vec2 rg = mix( mix( rgA, rgB, f.x ),
                   mix( rgC, rgD, f.x ), f.y );
    return mix( rg.x, rg.y, f.z );
  #endif
}
#else
float noise( in vec3 pos )
{
    //  https://github.com/BrianSharpe/Wombat/blob/master/Value3D.glsl

    // establish our grid cell and unit position
    vec3 Pi = floor(pos);
    vec3 Pf = pos - Pi;
    vec3 Pf_min1 = Pf - 1.0;

    // clamp the domain
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    // calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec2 hash_mod = vec2( 1.0 / ( 635.298681 + vec2( Pi.z, Pi_inc1.z ) * 48.500388 ) );
    vec4 hash_lowz = fract( Pt * hash_mod.xxxx );
    vec4 hash_highz = fract( Pt * hash_mod.yyyy );

    //	blend the results and return
    vec3 blend = Pf * Pf * Pf * (Pf * (Pf * 6.0 - 15.0) + 10.0);
    vec4 res0 = mix( hash_lowz, hash_highz, blend.z );
    vec4 blend2 = vec4( blend.xy, vec2( 1.0 - blend.xy ) );
    return dot( res0, blend2.zxzx * blend2.wwyy );
}
#endif

float ridged_noise(in vec3 p) { return abs(from_01_to_11(noise(p))); }

const mat3 m = mat3( 0.00,  0.80,  0.60,
                    -0.80,  0.36, -0.48,
                    -0.60, -0.48,  0.64 );

float fbm(vec3 p, int octaves, float lacunarity, float gain) 
{
    float
    total = 0.0,
    amplitude = gain,
    sum_amplitude = 0.0
    ;
    
	for (int i = 0; i < octaves; i++) 
    {
		total += noise(p) * amplitude;
		p = m * p * lacunarity;
        sum_amplitude += amplitude;
		amplitude *= gain;
	}
    
	return total / sum_amplitude;
}

float ridged_fbm(vec3 p, int octaves, float lacunarity, float gain) 
{
    float
    total = 0.0,
    amplitude = gain,
    sum_amplitude = 0.0
    ;
    
	for (int i = 0; i < octaves; i++) 
    {
		total += ridged_noise(p) * amplitude;
		p = m * p * lacunarity;
        sum_amplitude += amplitude;
		amplitude *= gain;
	}
    
	return total / sum_amplitude;
}

vec4 debug_display(float v)
{
    v = clamp(v, 0., 1.);
    return vec4
    (
        step(0.0, v)*v*(vec3(0.323,0.915,0.340)) 
      - step(v, 0.0)*v*(vec3(0.895,0.311,0.217)),
        1.0
    );
}

vec2 screen_point(in vec2 fragCoord)
{
    return (-iResolution.xy + 2.0*fragCoord.xy) / iResolution.y;
}

bool camera(in vec2 fragCoord, out vec3 point, out vec3 normal, out vec3 ray_direction)
{
    const float _StartingAngle = -100.088;
    const float _StartingZoom = 1.672;
    const float _Fov = 10.000;
    const float _RotationSpeed = 0.04;

    vec2 p = screen_point(fragCoord);
    vec2 m = screen_point(iMouse.xy);
    //_Warp = iMouse.w *0.01;
    
    // camera movement
    float an = _StartingAngle;
	an += iTime * _RotationSpeed;
    an += iMouse.x * 0.02;
    
    float zoom = _StartingZoom*_Fov;
    zoom += from_01_to_11(m.y) * 3.8;
    
	vec3 ray_origin = vec3(zoom*cos(an), 0.0, zoom*sin(an));
    vec3 look_at = vec3(0.0, 0., 0.0);
    // camera matrix
    vec3 ww = normalize(look_at - ray_origin);
    vec3 uu = normalize(cross(ww,vec3(0.0,1.0,0.0)));
    vec3 vv = normalize(cross(uu,ww));
	// create view ray
	ray_direction = normalize(p.x*uu + p.y*vv + _Fov*ww);

    // sphere center
	vec3 sphere_center = vec3(0.0,0.0,0.0);
    
    // raytrace a sphere
	vec3 center_direction = ray_origin - sphere_center;
	float b = dot(ray_direction, center_direction);
	float c = dot(center_direction, center_direction) - 1.0;
	float d = b*b - c;
	
    if (d <= 0.0) // miss
    {
        return false;
    }
	
    d = -b - sqrt(d);

    point = ray_origin + d*ray_direction;
    normal = normalize(point - sphere_center);
    
    return true;
}

const vec3 sun_direction = normalize(vec3(2.0, 1.0, 0.0));


void clouds_shape(in vec3 sampling_point, in float raise, out float shape, out float ridged_noise, out float regular_noise)
{
    const float _Coverage = -0.060;
	const float _Rigidness = 1.972 * 10.;
    const float _PolarBias = 1.260;

    ridged_noise = ridged_fbm(sampling_point, 12, 2.01, 0.610);
    regular_noise = fbm(sampling_point, 12, 2.01, 0.626);
    float rescaled_regular_noise = from_01_to_11(regular_noise);
    
    float r = 2.5*smoothbands(ridged_noise, 10., 0.096);
    float n = 1.260*smoothbands(rescaled_regular_noise, 10., 0.5);
    float h = smoothstep(0.7, 1.0, abs(raise));
    float base_shape = n*n*r*r;
    float adjusted_shape = _Rigidness*base_shape + _Coverage + _PolarBias * h;
    shape = adjusted_shape;
}

void clouds(in vec3 point, in vec3 normal, out float shape, out vec3 color, out float specular_intensity)
{
    const float _Scale = 2.148;
    const float _Squosh = 1.744;
    const float _Offset = 1.208;
    const float _Warp = 0.1;
	const vec3  _BrightFrontColor = vec3(.95,0.95,0.96);
	const vec3  _DarkFrontColor = vec3(0.629,0.672,0.740);
    const float _SpecularIntensity = 0.17;
    
    specular_intensity = _SpecularIntensity;
    
    vec3 sampling_point = point;
    sampling_point *= _Scale;
    sampling_point += vec3(iTime*0.01 );
	sampling_point += _Offset;
    sampling_point.y *= _Squosh;
    
    float warp_noise =_Warp * from_01_to_11((fbm(sampling_point * 2., 4, 2.010, 0.284)));
    sampling_point += warp_noise;
    
    float ridged_noise;
    float regular_noise;
    
    clouds_shape(sampling_point, point.y, shape, ridged_noise, regular_noise);
    if (shape <= 0.0)
    {
        shape = 0.0;
        return;
    }
    
    float color_intensity = smoothbands(smoothstep(0., 1.0, 0.528*(1.544*ridged_noise + regular_noise)), 10., 0.520);
    color = mix(_DarkFrontColor, _BrightFrontColor, color_intensity);
    
    //calculate shade: project the sun direction to the surface's normal and calculate the derivative
    vec3 shadow_direction = sun_direction - dot(sun_direction, normal)*normal;
    if (dot(shadow_direction, shadow_direction) > 0.0)
    {
        float diff_shape;
        clouds_shape(sampling_point + 0.004*shadow_direction, point.y, diff_shape, ridged_noise, regular_noise);
        float diff = shape - diff_shape;
        color += 0.04*diff;
    }
    
    shape = saturate(shape);
}

void surface(in vec3 point, out vec3 color, out float specular_intensity)
{
    const vec3  _OceanColor = vec3(0.201,0.404,0.605);
    const float _SpecularIntensity = 0.5;

    color = _OceanColor;
    specular_intensity = _SpecularIntensity;
}



float diffuse(in vec3 normal)
{
    return smoothstep(0., 1., 3.0*dot(normal, sun_direction));
}

vec3 specular(in vec3 normal, in vec3 ray_direction, in float intensity)
{
    const float _Shiness = 6.0;
    const vec3 _Color = vec3(1.,1.,0.770);

    vec3 view_direction = -ray_direction;

  	vec3 specular;
  	if (dot(normal, sun_direction) < 0.0) // light source on the wrong side?
    {
        specular = vec3(0.0); // no specular reflection
    }
  	else
    {
        specular = _Color*intensity*smoothstep(0., 1., pow(max(0.0, dot(reflect(-sun_direction, normal), view_direction)),_Shiness));
    }
    return specular;
}

vec3 light(in vec3 diffuse_color, in vec3 normal, in vec3 ray_direction, in float specular_intensity)
{
    float diffuse = diffuse(normal);
    vec3 specular = diffuse * specular(normal, ray_direction, specular_intensity);
    return diffuse_color * vec3(diffuse) + specular;
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
    vec3 point;
    vec3 normal;
    vec3 ray_direction;
    
    if(!camera(fragCoord, point, normal, ray_direction))
    {
        fragColor = vec4(0.0, 0.0, 0.0, 1.0);
        return;
    }
    
    float clouds_shape;
    vec3 clouds_color;
    float clouds_specular;
    clouds(point, normal, clouds_shape, clouds_color, clouds_specular);
    
    vec3 surface_color;
    float surface_specular;
    surface(point, surface_color, surface_specular);
    
    vec3 color = mix(surface_color, clouds_color, clouds_shape);
    float specular_intensity = mix(surface_specular, clouds_specular, clouds_shape);
    
    vec3 final_color = light(color, normal, ray_direction, specular_intensity);
    
    //fragColor = vec4(light, 1.0);
    //return;
    
    fragColor = vec4(final_color,1.0);
}