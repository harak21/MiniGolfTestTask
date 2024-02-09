Shader "Unlit/HoleMask"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque-5" }
        Blend Zero One
        ColorMask 0
        ZWrite On

        Pass{     }
    }
}
