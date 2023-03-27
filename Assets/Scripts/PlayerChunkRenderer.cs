using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChunkRenderer : MonoBehaviour
{
    [NonSerialized]
    public BoxCollider colliderToRenderChunks;
    
    public float renderDistance = 4;
    
    void Awake()
    {
        colliderToRenderChunks = GetComponent<BoxCollider>();
    }

    void Start()
    {
        colliderToRenderChunks.size = new Vector3(renderDistance * Chunk.CHUNK_SIZE, 1, renderDistance * Chunk.CHUNK_SIZE);
    }

    void OnTriggerEnter(Collider enteringCollider)
    {
        if (enteringCollider.GetComponentInParent<Chunk>() != null)
        {
            Chunk chunk = enteringCollider.GetComponentInParent<Chunk>();
            chunk.Render();
        }
    }
}
