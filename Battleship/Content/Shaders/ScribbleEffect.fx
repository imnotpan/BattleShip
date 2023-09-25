#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;

Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

float2 doodleMaxOffset;
float time;
float doodleFrameTime;
int doodleFrameCount;
float2 doodleNoiseScale;


float random(float2 seed)
{
    return frac(sin(dot(seed.xy, float2(12.9898, 78.233))) * 43758.5453123);
}
float noise(float2 seed)
{
    float2 i = floor(seed);
    float2 f = frac(seed);

    float a = random(i);
    float b = random(i + float2(1.0f, 0.0f));
    float c = random(i + float2(0.0f, 1.0f));
    float d = random(i + float2(1.0f, 1.0f));

    float2 u = f * f * (3.0f - 2.0f * f);

    return lerp(a, b, u.x) + (c - a) * u.y * (1.0f - u.x) + (d - b) * u.x * u.y;
}
float2 DoodleTextureOffset(float2 textureCoords, float2 maxOffset, float time, float frameTime, int frameCount, float2 noiseScale)
{
    float timeValue = (floor(time / frameTime) % frameCount) + 1;
    float2 offset = 0;
    offset.x = (noise((textureCoords + timeValue) * noiseScale.x) * 2.0 - 1.0) * maxOffset.x;
    offset.y = (noise((textureCoords + timeValue) * noiseScale.y) * 2.0 - 1.0) * maxOffset.y;
    return offset;
}

float4 frag(float2 pos : TEXCOORD0) : COLOR0
{
    float2 offset = 0.0;
    offset = DoodleTextureOffset(pos, doodleMaxOffset, time,
					doodleFrameTime, doodleFrameCount,
					doodleNoiseScale);
    float4 col = tex2D(SpriteTextureSampler, pos + offset);
    return col;
}


technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 frag();
    }
}
