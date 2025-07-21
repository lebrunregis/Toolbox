using System;

namespace DataStore.Runtime
{
    [Flags]
    public enum DataCategoryEnum
    {
        Save,
        Difficulty,
        Audio,
        Graphics,
        GameState,
        Achievements,
        Runtime,
        Player
    }
}
