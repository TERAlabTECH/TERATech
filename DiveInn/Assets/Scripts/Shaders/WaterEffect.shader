Shader "Unlit/WaterEffect"
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
        // AlphaTest Greater 0.5

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float resolutionFactor = 0.0009; // This can be adjusted to suit your needs

                // Adjust UV coordinates
                float2 adjustedUV = floor(i.uv / resolutionFactor) * resolutionFactor;

                float time = _Time.y * .1 + 23.0;
                float2 uv = adjustedUV;
                float2 p = fmod(uv * 6.28318530718, 6.28318530718)*4 - 250.0;
                float2 it = p;
                float c = 1.4;
                float inten = .009;
                for (int n = 0; n <9; n++)
                {
                    float t = time * (1.0 - (3.5 / float(n + 1)));
                    it = p + float2(cos(t - it.x) + sin(t + it.y), sin(t - it.y) + cos(t + it.x));
                    c += 1.0 / length(float2(p.x / (sin(it.x + t) / inten), p.y / (cos(it.y + t) / inten)));
                }
                c /= 5.0;
                c = 1.17 - pow(c, 1.4);
                float3 colour = pow(abs(c), 8.0);
                colour = clamp(colour + float3(0.0, 0.35, 0.5), 0.0, 1.);

                // sample the texture and modulate with calculated color
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb *= colour;
                

                col.a=.05;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
