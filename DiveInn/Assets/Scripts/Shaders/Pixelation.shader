Shader "Custom/Pixelation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelationAmount ("Pixelation Amount", Float) = 100.0
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

            sampler2D _MainTex;
            float _PixelationAmount;

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
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color; 
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Get texture resolution
                float2 texResolution = float2(_PixelationAmount, _PixelationAmount);

                // Adjust UVs to pixelate the texture
                float2 pixelatedUV = floor(i.uv * texResolution) / texResolution;

                // Sample the texture with pixelated UVs
                float4 col = tex2D(_MainTex, pixelatedUV);
                
                return col*i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
