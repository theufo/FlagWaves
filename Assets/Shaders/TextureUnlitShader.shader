Shader "Custom/TextureUnlitShader"
{
	Properties {
		_MainTex ("Texture", 2D) = "white" {}		
	}
	
    Subshader {
        
        Pass {
            CGPROGRAM

            #pragma vertex VertexProgram
			#pragma fragment FragmentProgram

            #include "UnityCG.cginc"

            float4 _Tint;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct Interpolators {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

            struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};
            
            Interpolators VertexProgram (VertexData v) {
            	Interpolators i;
				i.uv = TRANSFORM_TEX(v.uv, _MainTex);
				i.position = UnityObjectToClipPos(v.position);
				return i;
			}

			float4 FragmentProgram (Interpolators i) : SV_TARGET {
			    return tex2D(_MainTex, i.uv);
			}
            
            ENDCG
        }
    }
}