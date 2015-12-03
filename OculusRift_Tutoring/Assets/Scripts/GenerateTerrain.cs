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
                float xCos = Mathf.Cos(x);
                float zSin = -Mathf.Sin(z);
                //heights[x, z] = (xCos - zSin) / 100;
                heights[x, z] = Mathf.PerlinNoise((float)x / xSize * Tiling, (float)z / zSize * Tiling) / 10.0f;
            }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
