shader_type spatial;
render_mode unshaded;//, cull_disabled;

const vec4 _BgBottomColor = vec4(0.,0.,0.,0.9);
const vec4 _BgTopColor = vec4(0.,0.,0.,0.5);
const vec4 _EtherColor1 = vec4(0.,0.14,0.2,0.8);
const vec4 _EtherColor2 = vec4(0.,0.7,1.,1.);

void vertex () {
}

mat2 m(float a) {
	float c=cos(a), s=sin(a);
	return mat2(vec2(c,-s), vec2(s,c));
}

float mul(float a, float b) {
	return a * b;
}

float mul(vec2 a, vec2 b)
{
	return a.x * b.x + a.y * b.y;
}

vec2 mul(vec2 a, mat2 b)
{
	return vec2(mul(a, b[0]), mul(a, b[1]));
}

float map(vec3 p) {
	float t = TIME*0.5;
	p.xz = mul(p.xz,m(t*0.4));
	p.xy = mul(p.xy,m(t*0.3));
	vec3 q = p*2.+t;
	float s = sin(t*0.7);
	return length(p+vec3(s,s,s))*log(length(p)+1.) + sin(q.x+sin(q.z+sin(q.y)))*0.5 - 1.;
}

vec4 mainImage( vec2 fragCoord) {
	vec2 p = fragCoord.xy/1080.0f - vec2(.9,.5);
	vec3 cl = vec3(0.,0.,0.);
	float d = 2.5;
	for(int i=0; i<=5; i++)	{
		vec3 p2 = vec3(0,0,5.) + normalize(vec3(p.x,p.y, -1.))*d;
		float rz = map(p2);
		float f =  clamp((rz - map(p2+.1))*0.5, -.1, 1. );
		// vec3 l = vec3(0.1,0.3,.4) + vec3(5., 2.5, 3.)*f;
		vec3 l = vec3(_EtherColor1.r,_EtherColor1.g,_EtherColor1.b) + vec3(_EtherColor2.r,_EtherColor2.g,_EtherColor2.b)*f;
		cl = cl*l + (1.-smoothstep(0., 2.5, rz))*.7*l;
		d += min(rz, 1.);
	}
	return vec4(cl, 1.);
}

void fragment () {
	// sample the texture
	// fixed4 col = tex2D(_MainTex, i.uv);
	
	// Bottom to TopRight
	vec4 bgCol = mix(UV[0] * _BgTopColor, UV[1] * _BgBottomColor, UV[1]);
	
	// Bottom to Top
	// vec4 bgCol = lerp(_BgTopColor, _BgBottomColor, uv.y);
	//vec2 xx = (bgCol + mainImage(UV)) * vec4(1.,1.,1.,1);
	//vec2 yy = bgCol;

	ALBEDO = (bgCol + mainImage(VERTEX.xy)).xyz * vec3(1.,1.,0.5);
}
