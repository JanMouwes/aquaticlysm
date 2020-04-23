Shader "Textures/Outline"
{
    Properties
    {
        _Color("Main color", Color) = (0.5, 0.5, 0.5, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline color", Color) = (0, 0, 0, 1) // default color
        _OutlineWidth("Outline width", Range(1.0, 5.0)) = 1.01 // default width of the outline
    }

    CGINCLUDE
    #include "Unitycg.cginc"

        struct appdata{
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        struct v2f{
            float4 pos : POSITION;
            float3 normal : NORMAL;
        };

        float4 _OutlineColor;
        float _OutlineWidth;

        v2f vert(appdata v) 
        {
            v.vertex.xyz *= _OutlineWidth;

            v2f object;
            // Tranfer back to world space
            object.pos = UnityObjectToClipPos(v.vertex);

            return object;
        }

        ENDCG

            SubShader
        {
            Pass // Render the outline
            {
                ZWrite off // Don't write into buffer, so other things can appear on top of this

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                half4 frag(v2f i) : COLOR
                {
                    return _OutlineColor;
                }

                ENDCG
        }

        Pass // Normal render
        {
            ZWrite On

            Material
            {
                Diffuse[_Color]
                Ambient[_Color]
            }

            Lighting On

            SetTexture[_MainTex]
            {
                ConstantColor[_Color]
            }

            SetTexture[_MainText]
            {
                Combine previous * primary DOUBLE    
            }
        }
    }
}
