﻿using UnityEngine;
using System.Collections;

/**
 *  \file GenerateTerrain.cs
 *  \author Havranek Kevin, Pagny Jérémie 
 *  \brief Dans ce fichier se trouvent les fonctions qui permettent de générer un terrain. 
 */

public class GenerateTerrain : MonoBehaviour
{
    public float Tiling = 10.0f;  /*!< Sert à parametrer le lissage du terrain */

    public int grayScale;

    public Terrain terrain; /*!< L'objet terrain */
    public Shader shader;   /*!< Le shader pour texturer le terrain */

    public Texture2D grassTexture;  /*!< La texture herbe */
    public Texture2D stoneTexture;  /*!< La texture roche */
    public Texture2D snowTexture;   /*!< La texture neige */

	public Light sunLight;

    /**
     *  \brief La fonction qui est lancée automatiquement et qui appelle les fonctions 
     *         pour générer et texturer le terrain
     */   
    private void Start ()
    {
        UpdateTerrainHeight();
        UpdateTerrainTexture();
	}

    /**
     *  \brief Cette fonction génère les sommets du terrain grâce à l'algorithme de Perlin.
     */
    private void UpdateTerrainHeight()
    {
        int xSize = terrain.terrainData.heightmapWidth;
        int zSize = terrain.terrainData.heightmapHeight;
        System.Random rand = new System.Random();
        float seed = rand.Next(0, 50);
        float[,] heights = terrain.terrainData.GetHeights(0, 0, xSize, zSize);
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                heights[x, z] = Mathf.PerlinNoise((seed + x) / xSize * Tiling, (seed + z) / zSize * Tiling) / 10.0f;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    /**
     *  \brief Cette fonction texture le terrain via un shader
     */
    private void UpdateTerrainTexture()
    {
        Material material = new Material(shader);

        //Texture data
        material.SetTexture("_TextureGrass", grassTexture);
        material.SetTexture("_TextureStone", stoneTexture);
        material.SetTexture("_TextureSnow", snowTexture);

        //GrayScale data
        material.SetTexture("_GrayScaleTexture", GenerateGrayScaleTexture());
        material.SetInt("_GrayScale", grayScale);

        //Light data
        material.SetVector("_LightColor", sunLight.color);
        material.SetVector("_LightDir", sunLight.transform.forward);
        material.SetFloat("_LightIntensity", sunLight.intensity);

        terrain.materialType = Terrain.MaterialType.Custom;
        terrain.materialTemplate = material;
    }

    private Texture2D GenerateGrayScaleTexture()
    {
        Texture2D texture = new Texture2D(grayScale, 1);
        texture.filterMode = FilterMode.Point;

        float level = 1.0f / grayScale;
        for (int i = 0; i < grayScale; i++)
        {
            float Y = level * (i + 1);
            Debug.Log(Y);
            texture.SetPixel(i, 1, new Color(Y, Y, Y));
        }
        texture.Apply();

        return texture;
    }
}
