using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Material voxelMaterial;
    public float[,] heightMap;
    public ChunkMeshData chunkMeshData;

    public static readonly int CHUNK_SIZE = 32;
    public static readonly int CHUNK_HEIGHT = 256;

    Block[,,] _blocks = new Block[CHUNK_SIZE, CHUNK_HEIGHT, CHUNK_SIZE];

    public void FillBlocksForChunk()
    {
        for (int y = 0; y < CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int z = 0; z < CHUNK_SIZE; z++)
                {
                    if (y < heightMap[x, z])
                    {
                        _blocks[x, y, z] = new Block(BlockTypes.LAND, true, true);
                    }
                    else
                    {
                        _blocks[x, y, z] = new Block(BlockTypes.AIR, false, false);
                    }
                }
            }
        }
    }

    public void CreateBlockedMesh()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.MarkDynamic();

        int numVoxels = CHUNK_SIZE * CHUNK_HEIGHT * CHUNK_SIZE;
        int numVertices = numVoxels * 8;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];

        int vertexIndex = 0;

        chunkMeshData = new ChunkMeshData();
        mesh.vertices = chunkMeshData.GetVerticesForChunk();
        mesh.triangles = chunkMeshData.GetTrianglesForChunk(mesh.vertices, heightMap);

        gameObject.transform.position = new Vector3(0, 1, 0);
        gameObject.AddComponent<MeshCollider>();
    }

    public static Chunk InstantiateNewChunk(float[,] heightMap)
    {
        GameObject newChunkGameObject = new GameObject();
        newChunkGameObject.name = "Chunk";
        Chunk newChunk = newChunkGameObject.AddComponent<Chunk>();

        newChunk.heightMap = heightMap;
        newChunk.FillBlocksForChunk();
        newChunk.CreateBlockedMesh();

        return newChunk;
    }

}
