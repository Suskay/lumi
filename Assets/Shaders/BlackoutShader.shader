Shader "Custom/ClosingVignetteShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0.1) // Black with some opacity
        _ClosingRadius ("Closing Radius", Float) = 1.0 // Start with the smallest coverage
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

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

            fixed4 _Color;
            float _ClosingRadius;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv * 2 - 1; // Map the uv coordinates to -1 to 1 range
                float d = length(uv); // Calculate the distance from the center
                float vignette = 1 - step(_ClosingRadius, d); // Corrected vignette calculation
                return _Color * (1 - vignette); // Invert the vignette
            }
            ENDCG
        }
    }
}