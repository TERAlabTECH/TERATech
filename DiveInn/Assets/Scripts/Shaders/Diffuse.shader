Shader "Custom/FadeAlphaFromCenter"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)  // Solid color of the rectangle
        _RectCenter ("Rectangle Center", Vector) = (0.5, 0.5, 0, 0) // UV coordinates for center
        _RectSize ("Rectangle Size", Vector) = (0.2, 0.2, 0, 0) // Size of the rectangle (width, height)
        _FadeDistance ("Fade Distance", Float) = 0.5 // The distance over which the alpha fades
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
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
            float4 _RectCenter; // UV coordinates for the rectangle's center
            float4 _RectSize;   // Rectangle size (width and height)
            float _FadeDistance; // Distance from the center at which the alpha starts fading

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader to fade alpha based on distance from the rectangle's center
            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the relative distance of the current pixel from the rectangle center in UV space
                float2 uv = i.uv;
                float2 rectCenter = _RectCenter.xy;
                float2 rectHalfSize = _RectSize.xy * 0.5;

                // Calculate the distance from the center of the rectangle
                float2 distFromCenter = abs(uv - rectCenter);

                // Check if we're within the rectangle bounds
                if (distFromCenter.x < rectHalfSize.x && distFromCenter.y < rectHalfSize.y)
                {
                    // Inside the rectangle, no alpha fading
                    return _Color;
                }
                else
                {
                    // Calculate how far outside the rectangle bounds we are
                    float fadeFactor = length(distFromCenter - rectHalfSize) / _FadeDistance;

                    // Ensure fadeFactor is between 0 and 1
                    fadeFactor = clamp(fadeFactor, 0.0, 1.0);

                    // Reduce the alpha based on the fade factor
                    float alpha = _Color.a * (1.0 - fadeFactor);

                    return fixed4(_Color.rgb, alpha);
                }
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
