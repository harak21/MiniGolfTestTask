Shader "Unlit/Hole"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque-10 "
        }

        Pass
        {
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            float4 _Color;

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 nl : Tecoord1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                const half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.nl = max(0, dot(worldNormal, float3(0,0,1)));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return lerp(_Color, _Color*0.3, i.nl);
                return _Color * i.nl + _Color * 0.5 * (1-i.nl);
            }
            ENDCG
        }
    }
}