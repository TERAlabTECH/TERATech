Shader "Unlit/WaterEffect2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent+100" "RenderType"="Transparent" "IgnoreProjector"="True" }

        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Length squared of a 2D vector
            float length2(float2 p) {
                return dot(p, p);
            }

            // Noise function for scattering points
            float noise(float2 p) {
                return frac(sin(frac(sin(p.x) * 43.13311) + p.y) * 31.0011);
            }

            // Worley noise calculation
            float worley(float2 p) {
                float d = 1e30; // Set distance to a very high value initially
                for (int xo = -1; xo <= 1; ++xo) {
                    for (int yo = -1; yo <= 1; ++yo) {
                        float2 tp = floor(p) + float2(xo, yo);
                        d = min(d, length2(p - tp - noise(tp)));
                    }
                }
                return 3.0 * exp(-4.0 * abs(2.5 * d - 1.0));
            }

            // Fractal worley noise
            float fworley(float2 p, float time) {
                return sqrt(sqrt(sqrt(
                    worley(p * 5.0 + 0.05 * time) *
                    sqrt(worley(p * 50.0 + 0.12 + -0.1 * time)) *
                    sqrt(sqrt(worley(p * -10.0 + 0.03 * time))))));
            }

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Time variable in Unity
                float time = _Time.y * 2.4;
                
                // Normalized UV coordinates
                float resolutionFactor = 0.0014; // This can be adjusted to suit your needs

                // Adjust UV coordinates
                float2 adjustedUV = floor(i.uv / resolutionFactor) * resolutionFactor;
                float2 uv =adjustedUV;

                // Fractal worley noise computation
                float t = fworley(uv * 1.0, time);

                

                // Create a blue color modulation
                float3 colour = t * float3(0.1, 1.1 * t, pow(t, 0.5 - t));

                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                
                
                // Modulate the texture color by noise
                col.rgb *= colour;

                // col.rgb= clamp(col +float3(0.0,0.0,0.0), 0,1.0);
                col.g-=0.2;
                col.b+=0.4;
                col.a = 0.035; // Adjust alpha for transparency
                
                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                return col;
            }
            ENDCG
        }
    }
}
