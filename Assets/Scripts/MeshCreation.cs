using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreation : MonoBehaviour
{
    float seed = 0;
    float flattenFactor = 0.01f;

    Dictionary<Vector3, Chunk> loadedChunks;

    float worldSize = 3;

    void Awake()
    {
        loadedChunks = new Dictionary<Vector3, Chunk>();
    }

    void Start()
    {
        for (int i = 0; i < worldSize; i++)
        {
            for (int j = 0; j < worldSize; j++)
            {
                CreateNewChunkAt(new Vector3(i * Chunk.CHUNK_SIZE, 0, j * Chunk.CHUNK_SIZE));
            }
        }
    }

    void CreateNewChunkAt(Vector3 chunkPosition)
    {
        float[,] heightMap = new float[Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE];
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int z = 0; z < Chunk.CHUNK_SIZE; z++)
            {
                Vector3 worldPosition = chunkPosition + new Vector3(x, 0, z);

                heightMap[x, z] = Mathf.Floor(5 * Mathf.PerlinNoise(worldPosition.x * 0.1f, worldPosition.z * 0.1f));
            }
        }

        loadedChunks.Add(chunkPosition, Chunk.InstantiateNewChunk(chunkPosition, heightMap));
    }
}
