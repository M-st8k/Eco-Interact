Shader "Custom/CircularStencilShader"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _StencilTex("Stencil", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _StencilTex;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                fixed4 stencil = tex2D(_StencilTex, i.texcoord);
                clip(stencil.a - 0.5); // Clip if the stencil texture's alpha is less than 0.5 (circle mask).
                return col * i.color;
            }
            ENDCG
        }
    }
}
