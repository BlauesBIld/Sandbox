using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkMeshData
{
    
    public Vector3[] GetVerticesForChunk()
    {
        List<Vector3> verticesToAdd = new List<Vector3>();

        for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int z = 0; z < Chunk.CHUNK_SIZE; z++)
                {
                    verticesToAdd.Add(new Vector3(x, y, z));
                }
            }
        }
        return verticesToAdd.ToArray();
    }


    public int[] GetTrianglesForChunk(Vector3[] vertices, float[,] heightMap)
    {
        List<Vector3> verticesList = vertices.ToList();
        List<int> trianglesIndices = new List<int>();
        for (int x = 0; x < Chunk.CHUNK_SIZE-1; x++)
        {
            for (int z = 0; z < Chunk.CHUNK_SIZE-1; z++)
            {
                AddTrianglesOnTop(heightMap, trianglesIndices, verticesList, x, z);
                AddTrianglesForNextCoordinates(heightMap, trianglesIndices, verticesList, x, z);
            }
        }
        return trianglesIndices.ToArray();
    }
    
    private void AddTrianglesForNextCoordinates(float[,] heightMap, List<int> trianglesIndices, List<Vector3> verticesList, int x, int z)
    {
        
        float differenceInHeightZ = heightMap[x, z + 1];
        for (int i = 0; i < Mathf.Abs(differenceInHeightZ); i++)
        {
            if (differenceInHeightZ < 0)
            {
                
            } else if (differenceInHeightZ > 0)
            {

            }
        }
    }
    
    private void AddTrianglesOnTop(float[,] heightMap, List<int> trianglesIndices, List<Vector3> verticesList, int x, int z)
    {
        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x, heightMap[x, z], z)));
        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x + 1, heightMap[x, z], z + 1)));
        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x + 1, heightMap[x, z], z)));

        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x, heightMap[x, z], z)));
        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x, heightMap[x, z], z + 1)));
        trianglesIndices.Add(verticesList.IndexOf(new Vector3(x + 1, heightMap[x, z], z + 1)));
    }
}
