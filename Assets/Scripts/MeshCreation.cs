using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreation : MonoBehaviour
{
    float seed = 0;
    float[,] heightMap;
    float flattenFactor = 0.01f;

    void Awake()
    {
        heightMap = new float[32, 32];
    }

    void Start()
    {
        for (int i = 0; i < Chunk._chunkSize; i++)
        {
            for (int j = 0; j < Chunk._chunkSize; j++)
            {
                heightMap[i, j] = Mathf.Floor(Mathf.PerlinNoise(i * flattenFactor + 0.1f, j * flattenFactor + 0.1f) * 256);
            }
        }

        Chunk newChunk = new Chunk();
        newChunk.FillBlocksForChunk(heightMap);
        
        CreateBlockedMesh();
    }
    
    void CreateBlockedMesh()
    {
        GameObject newMeshObj = new GameObject();
        newMeshObj.AddComponent<MeshFilter>();
        newMeshObj.AddComponent<MeshRenderer>();
        Mesh mesh = newMeshObj.GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        // make changes to the Mesh by creating arrays which contain the new values
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0)
        };
        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)
        };
        mesh.triangles = new int[]
        {
            0, 1, 2, 0, 2, 3
        };

        newMeshObj.transform.position = new Vector3(0, 1, 0);
    }
}
