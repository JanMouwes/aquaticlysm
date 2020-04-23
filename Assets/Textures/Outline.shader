/// <summary>
/// Unlit shader, without any lighting for highlighting selected units
/// </summary>

Shader "Textures/Outline"
{
    Properties
    {
        _Color("Main color", Color) = (0.5, 0.5, 0.5, 1.0) // default color
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline color", Color) = (0, 0, 0, 1) // default outlinecolor
        _OutlineWidth("Outline width", Range(1.0, 5.0)) = 1.01 // default width of the outline
    }

    CGINCLUDE
    #include "Unitycg.cginc"

        // vertex shader input
        struct appdata{
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        // vertex to fragment
        struct v2f{
            float4 pos : POSITION;
            float3 normal : NORMAL;
        };

        float4 _OutlineColor;
        float _OutlineWidth;

        // vertex shader
        v2f vert(appdata v) 
        {
            v.vertex.xyz *= _OutlineWidth;

            v2f object;
            // Transfer back to world space
            object.pos = UnityObjectToClipPos(v.vertex);

            return object;
        }

        ENDCG

            SubShader
        {
            // Make sure this shader is rendered before the background of the object.
            Tags{ "Queue" = "Transparent" } 

            //Render the outline, execution of the vertex and fragment code
            Pass
            {
                // Don't write into buffer, so the outlined object can appear on top of this
                ZWrite off

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
