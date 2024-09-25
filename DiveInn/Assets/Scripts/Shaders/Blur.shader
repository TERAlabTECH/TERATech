Shader "Unlit/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 0.005
        _MaxDistance ("Max Distance", Float) = 0.5
        _Center ("Gradient Center", Vector) = (0.5, 0.5, 0, 0)
        
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;
            float _MaxDistance;
            float4 _Center;
           
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }
            float shadowGradient(float2 uv, float2 center, float maxDistance)
            {
                // Calculate distance from the center of the UV space
                float dist = distance(uv, center);

                // Normalize the distance by dividing by the max distance (radius for full fade out)
                float normalizedDistance = saturate(dist / maxDistance);

                // Invert the normalized distance so that it becomes lighter towards the center
                float shadowStrength = 1.0 - normalizedDistance;

                return shadowStrength;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Shadow implementation
                
                if (col.a < 0.5)
                {
                    return fixed4(0.0, 0.0, 0.0, 0.0);
                }
                else
                {
                    fixed3 blurredColor = fixed3(0,0,0);
                    float2 uv = i.uv;
                    // Sampling offsets for simple blur
                    blurredColor += tex2D(_MainTex, uv + float2(-_BlurSize, _BlurSize)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(_BlurSize, _BlurSize)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(_BlurSize, -_BlurSize)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(-_BlurSize, -_BlurSize)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(-_BlurSize, 0)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(_BlurSize, 0)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(0, _BlurSize)).rgb;
                    blurredColor += tex2D(_MainTex, uv + float2(0, -_BlurSize)).rgb;

                    blurredColor /= 8; // Average the samples for blur
                    
                    blurredColor*= i.color;

                    float shadowStrenght= shadowGradient(i.uv, _Center, _MaxDistance);

                    blurredColor*= shadowStrenght;
                    

                    return fixed4(blurredColor, col.a);
                }
            }
            ENDCG
        }
    }
}