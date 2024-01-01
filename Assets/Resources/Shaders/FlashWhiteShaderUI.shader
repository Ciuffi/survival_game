Shader "Unlit/FlashWhiteShaderUI"
{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Stencil("Stencil", Range(0, 255)) = 0
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}

            Pass {
                Stencil {
                    Ref[_Stencil]
                    Comp always
                    Pass[_StencilOp]
                    Fail keep
                    ZFail keep
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
                #pragma multi_compile _ ETC1_UI_ALPHAMASK
                #pragma multi_compile _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_instancing
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
    #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
                    float4 stereoEyeIndex : TEXCOORD1;
    #endif
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
    #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
                    float4 stereoEyeIndex : TEXCOORD1;
    #endif
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
    #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
                    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(o);
    #endif
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.color = v.color * _Color;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;
                    col.a *= _Color.a;

                    return col;
                }
                ENDCG
            }
        }
            FallBack "Universal Render Pipeline/Unlit"
}