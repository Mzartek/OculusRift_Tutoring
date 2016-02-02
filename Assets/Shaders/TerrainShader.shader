Shader "TerrainShader"
{
	Properties
	{
		_TextureGrass("TextureGrass", 2D) = "white" {}
		_TextureStone("TextureStone", 2D) = "white" {}
		_TextureSnow("TextureSnow", 2D) = "white" {}
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _TextureGrass;
			sampler2D _TextureStone;
			sampler2D _TextureSnow;

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
				float height : HEIGHT;
			};

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
				fixed4 color;

				if (i.height < 40)
					color = tex2D(_TextureGrass, i.texcoord);
				else if (i.height < 60)
					color = tex2D(_TextureStone, i.texcoord);
				else
					color = tex2D(_TextureSnow, i.texcoord);

				return color;
			}

			ENDCG
		}
	}
}
