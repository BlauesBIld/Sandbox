using System.Collections.Generic;
using System.Linq;
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

    public Material GetMaterial()
    {
        return material;
    }
}