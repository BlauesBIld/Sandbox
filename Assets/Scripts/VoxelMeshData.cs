using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class VoxelMeshData
{
    private Vector3 position;
    private Material material;

    public VoxelMeshData(Vector3 position, Material material)
    {
        this.position = position;
        this.material = material;
    }

    public void AddVertices(Vector3[] vertices, Vector2[] uvs, int startIndex)
    {
        // Define the 24 vertices for the voxel
        Vector3[] voxelVertices = new Vector3[]
        {
            // Front face
            new Vector3(0, 0, 1) + position,
            new Vector3(1, 0, 1) + position,
            new Vector3(1, 1, 1) + position,
            new Vector3(0, 1, 1) + position,
            // Back face
            new Vector3(1, 0, 0) + position,
            new Vector3(0, 0, 0) + position,
            new Vector3(0, 1, 0) + position,
            new Vector3(1, 1, 0) + position,
            // Left face
            new Vector3(0, 0, 0) + position,
            new Vector3(0, 0, 1) + position,
            new Vector3(0, 1, 1) + position,
            new Vector3(0, 1, 0) + position,
            // Right face
            new Vector3(1, 0, 1) + position,
            new Vector3(1, 0, 0) + position,
            new Vector3(1, 1, 0) + position,
            new Vector3(1, 1, 1) + position,
            // Top face
            new Vector3(0, 1, 1) + position,
            new Vector3(1, 1, 1) + position,
            new Vector3(1, 1, 0) + position,
            new Vector3(0, 1, 0) + position,
            // Bottom face
            new Vector3(0, 0, 0) + position,
            new Vector3(1, 0, 0) + position,
            new Vector3(1, 0, 1) + position,
            new Vector3(0, 0, 1) + position,
        };

        // Define the UV coordinates for the vertices
        Vector2[] voxelUvs = new Vector2[]
        {
            // Front face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            // Back face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            // Left face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            // Right face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            // Top face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            // Bottom face
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
        };

        for (int i = 0; i < voxelVertices.Length; i++)
        {
            vertices[startIndex + i] = voxelVertices[i];
            uvs[startIndex + i] = voxelUvs[i];
        }
    }

    public void AddTriangles(int[] triangles, int startIndex)
    {
        // Define the triangles for the voxel mesh
        int[] voxelTriangles = new int[]
        {
            0, 2, 1, // Front face
            0, 3, 2,
            4, 6, 5, // Back face
            4, 7, 6,
            8, 10, 9, // Left face
            8, 11, 10,
            12, 14, 13, // Right face
            12, 15, 14,
            16, 18, 17, // Top face
            16, 19, 18,
            20, 22, 21, // Bottom face
            20, 23, 22,
        };

        // Add the triangles to the list
        for (int i = 0; i < voxelTriangles.Length; i++)
        {
            triangles[startIndex + i] = voxelTriangles[i] + startIndex;
        }
    }

    public Material GetMaterial()
    {
        return material;
    }
}