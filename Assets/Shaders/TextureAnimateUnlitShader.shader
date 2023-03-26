Shader "Custom/TextureAnimateUnlitShader"
{
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_AnimateX("Animate X", Vector) = (0, 0, 0, 0)
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
            float4 _AnimateX;
            
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
            	i.uv += frac(_AnimateX.x * float2(_Time.y, 0));
            	
				return i;
			}

			float4 FragmentProgram (Interpolators i) : SV_TARGET {
			    return tex2D(_MainTex, i.uv);
			}
            
            ENDCG
        }
    }
}