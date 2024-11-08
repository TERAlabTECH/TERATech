Shader "Custom/ShootingStars"
{
    Properties
    {
        _Speed("Speed", float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            // Define the shader model
            #pragma target 3.0
            // Specify the vertex and fragment shaders
            #pragma vertex vert
            #pragma fragment frag

            // Include Unity's shader library
            #include "UnityCG.cginc"

            // Built-in variable containing screen parameters (width and height)
            // float4 _ScreenParams; // x = width, y = height, z = 1 + 1/width, w = 1 + 1/height

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv       : TEXCOORD0;
                float4 vertex   : SV_POSITION;
            };

            // Vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                // Transform the vertex to clip space
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Pass the UV coordinates to the fragment shader
                o.uv = v.uv;
                return o;
            }
            float _Speed;

            // Fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                // Initialize the output color
                float4 outColor = float4(0.0, 0.0, 0.0, 1.0);

                // Compute the fragment coordinates in pixel space
                float2 fragCoord = i.uv * _ScreenParams.xy;

                // Normalize coordinates based on the screen height
                float2 st = fragCoord / _ScreenParams.y;

                // Line dimensions (box)
                float2 b = float2(0.0, 0.1);

                float dist;
                float2 loopST;
                float2x2 rotation;

                // Loop from 0.9 to 20.9 in increments of 1.0
                for (float idx = 0.9; idx < 21.0; idx += 1.0)
                {
                    // Calculate rotation using cosine function
                    float4 cosValues = cos(idx + float4(0.0, 33.0, 11.0, 0.0));

                    // Construct the rotation matrix
                    rotation = float2x2(cosValues.x, cosValues.y, cosValues.z, cosValues.w);

                    // Scale the space
                    loopST = st * idx * 0.5;

                    // Translate downward over time
                    loopST.y += _Time.y * 1.2*_Speed;

                    // Create rotated squares
                    loopST = frac(mul(loopST, rotation)) - 0.5;

                    // Rotate again to align local and global down directions
                    loopST = mul(rotation, loopST);

                    // Calculate distance for color falloff
                    dist = distance(clamp(loopST, -b, b), loopST);

                    
                    // Accumulate color
                    outColor.rgb += (0.0001 / dist) * (cos(loopST.y / 0.1 + 1.0) + 1.0);
                    
                    
                    float colorSum = outColor.r + outColor.g + outColor.b;

                    outColor.a = step(0.0, colorSum);
                }

                // Ensure the color components are within the [0,1] range
                outColor = saturate(outColor);

                return outColor;
            }
            ENDCG
        }
    }
}
