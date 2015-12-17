Shader "Unlit/TerrainShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Main Color", Color) = (1.0,.0,.0,.0)
	}
	SubShader
	{
		Pass {
			Material {
				Diffuse [_Color]
			}
			Lighting On
			
		}
	}
}
