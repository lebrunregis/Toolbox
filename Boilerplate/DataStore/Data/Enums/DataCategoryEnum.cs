using System;

namespace DataStore.Data
{
    [Flags]
    public enum DataCategoryEnum
    {
        Save = 1 << 0,
        Difficulty = 1 << 1,
        Audio = 1 << 2,
        Graphics = 1 << 3,
        GameState = 1 << 4,
        Achievements = 1 << 5,
        Runtime = 1 << 6,
        Player = 1 << 7
    }
}
