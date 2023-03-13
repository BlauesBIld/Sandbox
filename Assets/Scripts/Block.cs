using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockTypes
{
    AIR,
    LAND,
    STONE,
    ORE,
    DECORATION
}

public struct Block
{
    public BlockTypes BlockType
    {
        get;
        set;
    }

    public bool IsSolid
    {
        get;
        set;
    }

    public bool IsFullSized
    {
        get;
        set;
    }

    public Block(BlockTypes blockType, bool isSolid, bool isFullSized)
    {
        BlockType = blockType;
        IsSolid = isSolid;
        IsFullSized = isFullSized;
    }

    public void SetBlockType(BlockTypes blockType)
    {
        BlockType = blockType;
    }
}
