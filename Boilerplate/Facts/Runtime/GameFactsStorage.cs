using System;
using System.Runtime.Serialization;
using Facts.Data;
using SaveSystem.Runtime;
using UnityEngine;

namespace Facts.Runtime
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("booleans", booleans, typeof(FactDictionary<BooleanFactEnum, bool>));
            info.AddValue("strings", strings, typeof(FactDictionary<StringFactEnum, string>));
            info.AddValue("ints", ints, typeof(FactDictionary<IntFactEnum, int>));
            info.AddValue("floats", floats, typeof(FactDictionary<FloatFactEnum, float>));
            info.AddValue("transforms", transforms, typeof(FactDictionary<TransformFactEnum, Transform>));
            info.AddValue("gameobjects", gameobjects, typeof(FactDictionary<GameObjectFactEnum, GameObject>));
            info.AddValue("components", components, typeof(FactDictionary<ComponentFactEnum, Component>));
            info.AddValue("serializable", serializable, typeof(FactDictionary<ComponentFactEnum, ISerializationCallbackReceiver>));
        }

       // public SaveFile Serialize()
        //{

        //}

        public void Deserialize(SaveFile file)
        {

        }

    }
}
