Shader "Hidden/BlurSpriteWithAlphaFix"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 10)) = 2.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _BlurAmount;

            // Function to blur the image, but only sample non-transparent pixels
            fixed4 blur(sampler2D tex, float2 uv, float blurAmount)
            {
                fixed4 sum = fixed4(0.0, 0.0, 0.0, 0.0);
                float alphaSum = 0.0;
                float2 offsets[9] = {float2(-1, -1), float2(0, -1), float2(1, -1),
                                     float2(-1,  0), float2(0,  0), float2(1,  0),
                                     float2(-1,  1), float2(0,  1), float2(1,  1)};
                
                for (int i = 0; i < 9; i++)
                {
                    float2 sampleUV = uv + offsets[i] * blurAmount * _ScreenParams.zw;
                    fixed4 sampledCol = tex2D(tex, sampleUV);
                    
                    // Only add the pixel to the blur sum if its alpha is non-zero
                    if (sampledCol.a > 0.0)
                    {
                        sum += sampledCol;
                        alphaSum += 1.0; // Track how many samples were valid
                    }
                }

                // Avoid dividing by zero in case none of the pixels had valid alpha
                if (alphaSum > 0.0)
                    sum /= alphaSum;

                return sum;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Only blur the interior of the sprite, keep the edges sharp
                if (col.a > 0.0) // Preserve sharp edges where alpha > 0
                {
                    fixed4 blurredCol = blur(_MainTex, i.uv, _BlurAmount);
                    col.rgb = blurredCol.rgb; // Apply blurred color
                }

                return col;
            }
            ENDCG
        }
    }
}
