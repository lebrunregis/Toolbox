using System;
using Facts.Data;
using UnityEngine;

namespace SaveSystem.Runtime
{
    [Serializable]
    public class GameFactsStorage : MonoBehaviour
    {
        public static FactDictionary<BooleanFactEnum, bool> booleans = new();
        public static FactDictionary<StringFactEnum, string> strings = new();
        public static FactDictionary<IntFactEnum, int> ints = new();
        public static FactDictionary<IntFactEnum, Enum> enums = new();
        public static FactDictionary<FloatFactEnum, float> floats = new();
        public static FactDictionary<TransformFactEnum, Transform> transforms = new();
        public static FactDictionary<GameObjectFactEnum, GameObject> gameobjects = new();
        public static FactDictionary<ComponentFactEnum, Component> components = new();
        public static FactDictionary<ComponentFactEnum, ISerializationCallbackReceiver> serializable = new();

        public void Clear()
        {
            booleans.Clear();
            strings.Clear();
            ints.Clear();
            enums.Clear();
            floats.Clear();
            transforms.Clear();
            gameobjects.Clear();
            components.Clear();
            serializable.Clear();
        }

    }
}
