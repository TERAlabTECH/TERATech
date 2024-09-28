Shader "Custom/FractalNoiseShaderWithPixelation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaValue("AlphaValue", float) = 0.3
        _DiverPosition ("Diver Position", Vector) = (0,0,0,0)
        _Radius("FogRadius", float) = 1
        _Pixelation("Pixelation", Float) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR; 
                float4 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                o.worldPos = float4(mul(unity_ObjectToWorld, v.vertex).xyz, 0); //calculates world pos
                return o;
            }

            sampler2D _MainTex;
            float _AlphaValue;
            float4 _DiverPosition;
            float _Radius;
            float _Pixelation;

            float rand(float2 p)
            {
                return frac(sin(dot(p, float2(200.99, 78.233))) * 56758.5453);
            }

            float noise(float2 p)
            {
                float2 f = frac(p);
                f = f * f * (3.0 - 2.0 * f);
                float2 i = floor(p);
                return lerp(lerp(rand(i + float2(0.0, 0.0)), 
                                 rand(i + float2(1.0, 0.0)), f.x),
                            lerp(rand(i + float2(0.0, 1.0)), 
                                 rand(i + float2(1.0, 1.0)), f.x), f.y);
            }

            float fbm(float2 p)
            {
                float v = 0.0;
                float a = 1.0;
                for(int i = 0; i < 4; ++i)
                {
                    p = 1.8 * p + 15.;
                    a *= 0.5;
                    v += a * noise(p);
                }
                return v;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Pixelation effect
                float2 pixelatedUV = floor(i.uv * _Pixelation) / _Pixelation;

                float2 p = 2. * pixelatedUV;
                float2 r1 = float2(fbm(p + 0.01 * _Time), fbm(p + 0.005 * _Time));
                float2 r2 = float2(fbm(p + 0.05 * _Time + 10. * r1), fbm(p + 0.12 * _Time + 12. * r1));
                float4 result = float4(1.8 * pow(fbm(p + r2), 2.) + 0.03, 1.8 * pow(fbm(p + r2), 2.) + 0.03, 1.8 * pow(fbm(p + r2), 2.) + 0.03, 1.0);

                result *= i.color;
                result.a = _AlphaValue;

                float dist = distance(_DiverPosition.xy, i.worldPos.xy);
                float alphaRadius = smoothstep(0.0, _Radius, dist);
                result.a = smoothstep(-0.02, result.a, alphaRadius);

                return result;
            }
            ENDCG
            //
        }
    }
    FallBack "Diffuse"
}
