﻿Shader "TerrainShader"
{
	Properties
	{
		_TextureGrass("TextureGrass", 2D) = "white" {}
		_TextureStone("TextureStone", 2D) = "white" {}
		_TextureSnow("TextureSnow", 2D) = "white" {}
		_LightColor("LightColor", Color) = (0, 0, 0, 0)
		_LightDir("LightDir", Vector) = (0, 0, 0, 0)
	}

		SubShader
	{
		Pass
		{
			Tags{ "RenderType" = "Opaque" "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _TextureGrass;
			sampler2D _TextureStone;
			sampler2D _TextureSnow;
			float4 _LightColor;
			float4 _LightDir;

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
				float height : HEIGHT;
			};

			struct SurfaceInput
			{
				float4 color : COLOR;
			};

			float testFunc(float value, float min, float max)
			{
				return clamp((value - min) / (max - min), 0, 1);
			}

			v2f vert(appdata_base v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = mul(UNITY_MATRIX_IT_MV, v.normal);
				o.texcoord = v.texcoord * 10;
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
					fixed4 grassColor = tex2D(_TextureGrass, i.texcoord) * testFunc(i.height, limit_1, 0);
					fixed4 stoneColor = tex2D(_TextureStone, i.texcoord) * testFunc(i.height, 0, limit_1);

					finalColor = grassColor + stoneColor;
				}
				else
				{
					fixed4 stoneColor = tex2D(_TextureStone, i.texcoord) * testFunc(i.height, limit_2, limit_1);
					fixed4 snowColor = tex2D(_TextureSnow, i.texcoord) * testFunc(i.height, limit_1, limit_2);

					finalColor = stoneColor + snowColor;
				}

				
				//return UNITY_LIGHTMODEL_AMBIENT * finalColor;
				return _LightColor * finalColor;
			}

			ENDCG
		}
	}
}
