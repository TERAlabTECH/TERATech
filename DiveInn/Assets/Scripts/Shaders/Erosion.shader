Shader "Custom/ErosionShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1) // Solid color to erode
        _ErosionSpeed ("Erosion Speed", Float) = 1.0 // Speed of erosion over time
        _Scale ("Noise Scale", Float) = 10.0 // Scale of the noise pattern
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

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

            // Uniforms
            fixed4 _Color;
            float _ErosionSpeed;
            float _Scale;
           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Simple 2D random noise function
            float random(float2 st)
            {
                return frac(sin(dot(st.xy, float2(12.9898,78.233))) * 43758.5453123);
            }

            // 2D Noise based on the random function
            float noise(float2 st)
            {
                float2 i = floor(st);
                float2 f = frac(st);

                float a = random(i);
                float b = random(i + float2(1.0, 0.0));
                float c = random(i + float2(0.0, 1.0));
                float d = random(i + float2(1.0, 1.0));

                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(a, b, u.x) +
                       (c - a) * u.y * (1.0 - u.x) +
                       (d - b) * u.x * u.y;
            }

            // Fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                // UV coordinates and time-driven erosion factor
                float2 uv = i.uv * _Scale;
                float erosion = noise(uv + _Time * _ErosionSpeed);

                // Use a threshold to make parts transparent
                float threshold = 0.5;
                float alpha = step(threshold, erosion);

                // Final color with erosion applied
                return fixed4(_Color.rgb, _Color.a * alpha);
            }
            ENDCG
        }
    }
    FallBack "Unlit/Color"
}
