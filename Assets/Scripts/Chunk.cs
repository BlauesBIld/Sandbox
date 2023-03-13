public class Chunk
{
    public static readonly int _chunkSize = 32;
    public static readonly int _chunkHeight = 256;
    
    Block[,,] _blocks = new Block[_chunkHeight,_chunkSize,_chunkSize];
    
    public void FillBlocksForChunk(float[,] heightMap){
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < _chunkSize; j++)
            {
                for (int k = 0; k < _chunkSize; k++)
                {
                    if (i < heightMap[j, k])
                    {
                        _blocks[i, j, k] = new Block(BlockTypes.LAND, true, true);
                    }
                    else
                    {
                        _blocks[i, j, k] = new Block(BlockTypes.AIR, false, false);
                    }
                }
            }
        }
    }
}
