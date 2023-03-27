using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Material voxelMaterial;
    public float[,] heightMap;
    public ChunkMeshData chunkMeshData;

    public static readonly int CHUNK_SIZE = 16;
    public static readonly int CHUNK_HEIGHT = 256;

    Block[,,] _blocks = new Block[CHUNK_SIZE, CHUNK_HEIGHT, CHUNK_SIZE];


    MeshRenderer meshRenderer;
    Mesh mesh;

    public GameObject meshObject;
    public GameObject renderColliderObject;
    
    private void Awake()
    {
        voxelMaterial = Resources.Load<Material>("Materials/DefaultVoxel");

        meshObject.AddComponent<MeshFilter>();
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        mesh = meshObject.GetComponent<MeshFilter>().mesh;
        
        meshObject.SetActive(false);
    }

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
        mesh.Clear();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.MarkDynamic();

        int vertexIndex = 0;

        chunkMeshData = new ChunkMeshData();
        mesh.vertices = chunkMeshData.GetVerticesForChunk();
        mesh.triangles = chunkMeshData.GetTrianglesForChunk(mesh.vertices, heightMap);
        meshRenderer.material = voxelMaterial;
        
        gameObject.AddComponent<MeshCollider>();
    }

    public static Chunk InstantiateNewChunk(Vector3 chunkPosition, float[,] heightMap)
    {
        GameObject newChunkGameObject = Instantiate(MeshCreationManager.instance.chunkPrefab, chunkPosition, Quaternion.identity);
        newChunkGameObject.name = "Chunk";
        
        Chunk newChunk = newChunkGameObject.GetComponent<Chunk>();
        newChunk.heightMap = heightMap;

        return newChunk;
    }

    public void Render()
    {
        Debug.Log("Rendering Chunk");
        meshObject.SetActive(true);
        renderColliderObject.SetActive(false);
        
        FillBlocksForChunk();
        CreateBlockedMesh();
        
        CreateNeighBourChunksIfNotExisting();
    }
    
    void CreateNeighBourChunksIfNotExisting()
    {
        Vector3 chunkPosition = transform.position;
        Vector3 chunkPositionXPlus = new Vector3(chunkPosition.x + CHUNK_SIZE, chunkPosition.y, chunkPosition.z);
        Vector3 chunkPositionXMinus = new Vector3(chunkPosition.x - CHUNK_SIZE, chunkPosition.y, chunkPosition.z);
        Vector3 chunkPositionZPlus = new Vector3(chunkPosition.x, chunkPosition.y, chunkPosition.z + CHUNK_SIZE);
        Vector3 chunkPositionZMinus = new Vector3(chunkPosition.x, chunkPosition.y, chunkPosition.z - CHUNK_SIZE);

        if (MeshCreationManager.instance.GetChunkAtPosition(chunkPositionXPlus) == null)
        {
            MeshCreationManager.instance.CreateNewChunkAt(chunkPositionXPlus);
        }
        if (MeshCreationManager.instance.GetChunkAtPosition(chunkPositionXMinus) == null)
        {
            MeshCreationManager.instance.CreateNewChunkAt(chunkPositionXMinus);
        }
        if (MeshCreationManager.instance.GetChunkAtPosition(chunkPositionZPlus) == null)
        {
            MeshCreationManager.instance.CreateNewChunkAt(chunkPositionZPlus);
        }
        if (MeshCreationManager.instance.GetChunkAtPosition(chunkPositionZMinus) == null)
        {
            MeshCreationManager.instance.CreateNewChunkAt(chunkPositionZMinus);
        }
    }
}
