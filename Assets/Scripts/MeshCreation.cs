using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreation : MonoBehaviour
{
    float seed = 0;
    float flattenFactor = 0.01f;

    Dictionary<Vector3, Chunk> loadedChunks;

    float renderDistance = 4;

    void Awake()
    {
        loadedChunks = new Dictionary<Vector3, Chunk>();
    }

    void Start()
    {
        float timestamp = Time.realtimeSinceStartup;
        for (int i = 0; i < renderDistance; i++)
        {
            for (int j = 0; j < renderDistance; j++)
            {
                CreateNewChunkAt(new Vector3(i * Chunk.CHUNK_SIZE, 0, j * Chunk.CHUNK_SIZE));
            }
        }
        
        Debug.Log("Created chunks in " + (Time.realtimeSinceStartup - timestamp) + " seconds");
    }

    void CreateNewChunkAt(Vector3 chunkPosition)
    {
        float[,] heightMap = new float[Chunk.CHUNK_SIZE+1, Chunk.CHUNK_SIZE+1];
        for (int x = 0; x < Chunk.CHUNK_SIZE+1; x++)
        {
            for (int z = 0; z < Chunk.CHUNK_SIZE+1; z++)
            {
                Vector3 worldPosition = chunkPosition + new Vector3(x, 0, z);

                heightMap[x, z] = 1+Mathf.Floor(5 * Mathf.PerlinNoise(worldPosition.x * 0.1f, worldPosition.z * 0.1f));
            }
        }

        loadedChunks.Add(chunkPosition, Chunk.InstantiateNewChunk(chunkPosition, heightMap));
    }
}
