using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {
    public float Tiling = 10.0f;

    public Terrain terrain;
    public Shader shader;

    public Texture2D grassTexture;
    public Texture2D stoneTexture;
    public Texture2D snowTexture;


    // Use this for initialization
    void Start ()
    {
        UpdateTerrainHeight();
        UpdateTerrainTexture();
	}

    void UpdateTerrainHeight()
    {
        int xSize = terrain.terrainData.heightmapWidth;
        int zSize = terrain.terrainData.heightmapHeight;
        float[,] heights = terrain.terrainData.GetHeights(0, 0, xSize, zSize);
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                heights[x, z] = Mathf.PerlinNoise((float)x / xSize * Tiling, (float)z / zSize * Tiling) / 10.0f;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    void UpdateTerrainTexture()
    {
        Material material = new Material(shader);
        material.SetTexture("_MainTex", grassTexture);

        terrain.materialType = Terrain.MaterialType.Custom;
        terrain.materialTemplate = material;
    }
}
