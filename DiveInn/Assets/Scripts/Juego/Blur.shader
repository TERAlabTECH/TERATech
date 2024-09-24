Shader "Unlit/BlurWithClampedEdges"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 1)) = 0.005
        _EdgeSharpness ("Edge Sharpness", Range(0, 1)) = 0.2
        _NumSamples ("Number of Blur Samples", Range(1, 20)) = 9
        _ClampUVs ("Clamp UVs", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
                float4 color : COLOR;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurAmount;
            float _EdgeSharpness;
            int _NumSamples;
            float _ClampUVs;  // Flag to control UV clamping

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float offset = _BlurAmount * 0.5;

                float2 offsets[9] = {
                    float2(-offset, -offset),
                    float2(0, -offset),
                    float2(offset, -offset),
                    float2(-offset, 0),
                    float2(0, 0),
                    float2(offset, 0),
                    float2(-offset, offset),
                    float2(0, offset),
                    float2(offset, offset)
                };

                fixed4 color = 0;

                // Iterate over the samples
                for (int j = 0; j < min(_NumSamples, 9); j++)
                {
                    float2 sampleUV = i.uv + offsets[j];

                    // Check if the UV is within the boundary (with a margin near edges)
                    if (_ClampUVs > 0)
                    {
                        if (sampleUV.x < 0.01 || sampleUV.x > 0.99 || sampleUV.y < 0.01 || sampleUV.y > 0.99)
                        {
                            // Ignore samples that are out of bounds or too close to the boundary
                            continue;
                        }
                    }

                    // Accumulate the sampled color
                    color += tex2D(_MainTex, sampleUV) * (1.0 / _NumSamples);
                }

                // Edge suppression: decrease blur at edges
                float edgeDistance = min(i.uv.x, 1.0 - i.uv.x); 
                edgeDistance = min(edgeDistance, min(i.uv.y, 1.0 - i.uv.y)); 
                float edgeFactor = smoothstep(0.0, _EdgeSharpness, edgeDistance);

                // Use the edge factor to reduce blur near edges
                color *= edgeFactor;

                // Multiply by vertex color to maintain tint
                color *= i.color;

                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, color);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
