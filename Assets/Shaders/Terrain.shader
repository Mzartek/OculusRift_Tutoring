Shader "Unlit/Terrain"
{
    Properties
    {
        _TextureGrass("TextureGrass", 2D) = "white" {}
        _TextureStone("TextureStone", 2D) = "white" {}
        _TextureSnow("TextureSnow", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uvGrass : TEXCOORD0;
                float2 uvStone : TEXCOORD1;
                float2 uvSnow : TEXCOORD2;
                float height : HEIGHT;
            };

            sampler2D _TextureGrass;
            sampler2D _TextureStone;
            sampler2D _TextureSnow;
            float4 _TextureGrass_ST;
            float4 _TextureStone_ST;
            float4 _TextureSnow_ST;

            float testFunc(float value, float min, float max)
            {
                return clamp((value - min) / (max - min), 0, 1);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.normal = mul(UNITY_MATRIX_IT_MV, v.normal);
                o.uvGrass = TRANSFORM_TEX(v.uv, _TextureGrass) * 10;
                o.uvStone = TRANSFORM_TEX(v.uv, _TextureStone) * 10;
                o.uvSnow = TRANSFORM_TEX(v.uv, _TextureSnow) * 10;
                o.height = v.vertex.y;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float limit_1 = 60;
                float limit_2 = 75;
                fixed4 finalColor;

                if (i.height < limit_1)
                {
                    finalColor  = tex2D(_TextureGrass, i.uvGrass) * testFunc(i.height, limit_1, 0);
                    finalColor += tex2D(_TextureStone, i.uvStone) * testFunc(i.height, 0, limit_1);
                }
                else
                {
                    finalColor  = tex2D(_TextureStone, i.uvStone) * testFunc(i.height, limit_2, limit_1);
                    finalColor += tex2D(_TextureSnow, i.uvSnow) * testFunc(i.height, limit_1, limit_2);
                }
                
                return UNITY_LIGHTMODEL_AMBIENT * finalColor;
            }
            ENDCG
        }
    }
}
