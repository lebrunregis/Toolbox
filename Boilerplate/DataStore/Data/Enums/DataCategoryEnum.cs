using System;

namespace DataStore.Runtime
{
    [Flags]
    public enum DataCategoryEnum
    {
        None = 0,
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
