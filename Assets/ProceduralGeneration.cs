using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class ProceduralGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public RuleTile tile;

    public int width, height;

    public Texture2D noiseTexture;

    [Range(0,1)]
    public float surfaceValue = 0.5f;
    public float caveFreq = 0.1f;

    private float seed;

    void Start()
    {
        seed = UnityEngine.Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    private void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiseTexture.Apply();
    }

    void GenerateTerrain()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (surfaceValue > noiseTexture.GetPixel(x, y).r)
                {
                    PlaceTile(tile, new Vector3Int(x, y));
                }
            }
        }
    }

    private void PlaceTile(RuleTile tile, Vector3Int position)
    {
        tilemap.SetTile(position, tile);
    }
}
