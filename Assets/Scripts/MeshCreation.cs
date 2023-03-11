using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreation : MonoBehaviour
{
    float chunkSize = 32;
    float seed = 0;
    float[,] heightMap;
    float flattenFactor = 0.01f;

    void Awake()
    {
        heightMap = new float[32, 32];
    }

    void Start()
    {
        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                heightMap[i, j] = Mathf.Floor(Mathf.PerlinNoise(i * flattenFactor + 0.1f, j * flattenFactor + 0.1f) * 256);
            }
        }

        CreateBlockedMesh();
    }
    void CreateBlockedMesh()
    {
        int[] triangles 
    }
}
