Shader "Custom/Jellyfish"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Alpha ("Alpha", Float) = 1.0
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 fC (float2 fragCoord, float2 iResolution)
            {
                float3 col = float3(0,0,0);
                float t = _Time * 0.05;
                float2 uv = (fragCoord - iResolution.xy)/ iResolution.y + float(t + 2.0);
                int c = 0;
                for(int i = 0; i < 9; i++)
                {
                    c = i % 3;
                    float factor = 1.5;
                    float2 uv1 = uv;
                    uv /= factor;
                    uv += uv1;
                    uv += (sin(uv.yx + _Time / 5.0)) / factor;
                    uv *= factor;
                    col[c] += sin(uv.x + uv.y);
                    col = col * float3(.45, .2, .9);
                }
                return float4(col * 10.0, _Alpha);
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 color = float4(0, 0, 0, 0);
                float A = 7.0;
                float s = 1.0 / A;
                float2 fragCoord = i.uv * _ScreenParams.xy;
                for (float x = -.5; x < .5; x += s)
                    for (float y = -.5; y < .5; y += s)
                        color += min(fC(fragCoord + float2(x, y), _ScreenParams.xy), 1.0);
                
                color /= (A * A);
                return color;
            }
            ENDCG
        }
    }
}
