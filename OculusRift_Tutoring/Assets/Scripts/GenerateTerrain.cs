using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {

    public Terrain terrain;
    public float Tiling = 10.0f;

    // Use this for initialization
    void Start () {
        GenTerrain();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenTerrain()
    {
        int xSize = terrain.terrainData.heightmapWidth;
        int zSize = terrain.terrainData.heightmapHeight;
        float[,] heights = terrain.terrainData.GetHeights(0, 0, xSize, zSize);

        for(int z = 0; z < zSize; z++)
            for(int x = 0; x < xSize; x++)
            {
                heights[x, z] = Mathf.PerlinNoise((float)x / xSize * Tiling, (float)z / zSize * Tiling) / 10.0f;
            }
        terrain.terrainData.SetHeights(0, 0, heights);
        UpdateTerrainTexture(terrain.terrainData, 2, 1);
    }

    void UpdateTerrainTexture(TerrainData terrainData, int textureNumberFrom, int textureNumberTo)
    {
        float[,,] alphas = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        for (int i = 0; i < terrainData.alphamapWidth; i++)
        {
            for (int j = 0; j < terrainData.alphamapHeight; j++)
            {
                alphas[i, j, textureNumberTo] = Mathf.Max(alphas[i, j, textureNumberFrom], alphas[i, j, textureNumberTo]);
                alphas[i, j, textureNumberFrom] = 0f;
            }
        }
        terrainData.SetAlphamaps(0, 0, alphas);
    }

}
