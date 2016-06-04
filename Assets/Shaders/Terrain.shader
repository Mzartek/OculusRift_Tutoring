Shader "Unlit/Terrain"
{
  Properties
  {
      _TextureGrass("TextureGrass", 2D) = "white" {}
      _TextureStone("TextureStone", 2D) = "white" {}
      _TextureSnow("TextureSnow", 2D) = "white" {}

      _GrayScaleTexture("GrayScaleTexture", 2D) = "white" {}
      _GrayScale("GrayScale", Int) = 0

      _LightColor("LightColor", Color) = (0, 0, 0, 0)
      _LightDir("LightDir", Vector) = (0, 0, 0, 0)
      _LightIntensity("LightIntensity", Float) = 0
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
                  float3 position : CAMERA_SPACE_POSITION;
                  float3 normal : NORMAL;
                  float height : HEIGHT;
                  float2 uvGrass : TEXCOORD0;
                  float2 uvStone : TEXCOORD1;
                  float2 uvSnow : TEXCOORD2;
              };

              sampler2D _TextureGrass;
              sampler2D _TextureStone;
              sampler2D _TextureSnow;
              float4 _TextureGrass_ST;
              float4 _TextureStone_ST;
              float4 _TextureSnow_ST;

              sampler2D _GrayScaleTexture;
              int _GrayScale;

              float4 _LightColor;
              float4 _LightDir;
              float _LightIntensity;

              float4 calcLight(float4 diffColor, float4 specColor, float3 N, float3 L, float3 V, float shininess)
              {
                  float3 H = normalize(L + V);
                  float4 diff = max(dot(N, L), 0.0) * diffColor;
                  float4 spec = pow(max(dot(N, H), 0.0), shininess) * specColor;
                  return diff + spec;
              }

              float testFunc(float value, float min, float max)
              {
                  return clamp((value - min) / (max - min), 0, 1);
              }

              v2f vert(appdata v)
              {
                  float textureRepeat = 20;
                  v2f o;
                  o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                  o.position = mul(UNITY_MATRIX_MV, v.vertex);
                  o.normal = mul(UNITY_MATRIX_IT_MV, v.normal);
                  o.height = v.vertex.y;
                  o.uvGrass = TRANSFORM_TEX(v.uv, _TextureGrass) * textureRepeat;
                  o.uvStone = TRANSFORM_TEX(v.uv, _TextureStone) * textureRepeat;
                  o.uvSnow = TRANSFORM_TEX(v.uv, _TextureSnow) * textureRepeat;

                  return o;
              }

              fixed4 frag(v2f i) : SV_Target
              {
                  float limit_1 = 60;
                  float limit_2 = 75;

                  fixed4 finalColor;
                  if (i.height < limit_1)
                  {
                      finalColor = tex2D(_TextureGrass, i.uvGrass) * testFunc(i.height, limit_1, 0);
                      finalColor += tex2D(_TextureStone, i.uvStone) * testFunc(i.height, 0, limit_1);
                  }
                  else
                  {
                      finalColor = tex2D(_TextureStone, i.uvStone) * testFunc(i.height, limit_2, limit_1);
                      finalColor += tex2D(_TextureSnow, i.uvSnow) * testFunc(i.height, limit_1, limit_2);
                  }

                  float3 L = -mul(UNITY_MATRIX_IT_MV, _LightDir);

                  fixed4 lightColor = UNITY_LIGHTMODEL_AMBIENT;
                  lightColor += calcLight(_LightColor, _LightColor, i.normal, L, normalize(i.position), 70);
                  lightColor *= _LightIntensity;

                  float intensity = max(dot(normalize(L), i.normal), 0);
                  finalColor *= tex2D(_GrayScaleTexture, float2(intensity, 0.0));

                  return finalColor;
              }
              ENDCG
          }
      }
}
