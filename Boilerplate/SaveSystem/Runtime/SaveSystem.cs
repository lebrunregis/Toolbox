using Facts.Data;
using UnityEngine;

namespace SaveSystem.Runtime
{
    public class SaveSystem : ISaveSystem
    {

        public SaveFile Save(GameFactsStorage gamestate)
        {
            SaveFile file = new();
            FactAdapter.ToList<BooleanFactEnum, bool>(GameFactsStorage.booleans, file.booleans);
            FactAdapter.ToList<StringFactEnum, string>(GameFactsStorage.strings, file.strings);
            FactAdapter.ToList<IntFactEnum, int>(GameFactsStorage.ints, file.ints);
            FactAdapter.ToList<FloatFactEnum, float>(GameFactsStorage.floats, file.floats);
            FactAdapter.ToList<TransformFactEnum, Transform>(GameFactsStorage.transforms, file.transforms);
            FactAdapter.ToList<GameObjectFactEnum, GameObject>(GameFactsStorage.gameobjects, file.gameobjects);
            FactAdapter.ToList<ComponentFactEnum, Component>(GameFactsStorage.components, file.components);
            FactAdapter.ToList<ComponentFactEnum, ISerializationCallbackReceiver>(GameFactsStorage.serializable, file.serializable);
            return file;
        }

        public void Load(SaveFile file)
        {
            FactAdapter.ToDictionary<BooleanFactEnum, bool>(file.booleans, GameFactsStorage.booleans);
            FactAdapter.ToDictionary<StringFactEnum, string>(file.strings, GameFactsStorage.strings);
            FactAdapter.ToDictionary<IntFactEnum, int>(file.ints, GameFactsStorage.ints);
            FactAdapter.ToDictionary<FloatFactEnum, float>(file.floats, GameFactsStorage.floats);
            FactAdapter.ToDictionary<TransformFactEnum, Transform>(file.transforms, GameFactsStorage.transforms);
            FactAdapter.ToDictionary<GameObjectFactEnum, GameObject>(file.gameobjects, GameFactsStorage.gameobjects);
            FactAdapter.ToDictionary<ComponentFactEnum, Component>(file.components, GameFactsStorage.components);
            FactAdapter.ToDictionary<ComponentFactEnum, ISerializationCallbackReceiver>(file.serializable, GameFactsStorage.serializable);
        }
    }
}
