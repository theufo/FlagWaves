Shader "Custom/WaveSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Amplitude ("Amplitude", Float) = 1
        _Speed ("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex: TEXCOORD0;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Amplitude, _Speed;

        void vert(inout appdata_full vertexData, out Input i)
        {
            // vertexData.texcoord.xy += frac(0.1 * float2(_Time.y, 0)); Uncomment to make texture move along x axis
            vertexData.vertex.z += _Amplitude * sin(vertexData.vertex.x - _Time * _Speed);

            UNITY_INITIALIZE_OUTPUT(Input, i);
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
