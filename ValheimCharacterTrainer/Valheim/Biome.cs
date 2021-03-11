namespace Valheim
{
    public enum Biome
    {
        None = 0,
        Meadows = 1,
        Swamp = 2,
        Mountain = 4,
        BlackForest = 8,
        Plains = 16, // 0x00000010
        AshLands = 32, // 0x00000020
        DeepNorth = 64, // 0x00000040
        Ocean = 256, // 0x00000100
        Mistlands = 512, // 0x00000200
        BiomesMax = 513, // 0x00000201
    }
}
