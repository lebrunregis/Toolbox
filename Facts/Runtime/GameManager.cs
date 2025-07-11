using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Facts.Runtime
{
    [Serializable]
    public class GameManager : MonoBehaviour
    {
        #region Publics
        public static FactDictionary<BooleanFactEnum, bool> booleans = new();
        public static FactDictionary<StringFactEnum, string> strings = new();
        public static FactDictionary<IntFactEnum, int> ints = new();
        public static FactDictionary<IntFactEnum, Enum> enums = new();
        public static FactDictionary<FloatFactEnum, float> floats = new();
        public static FactDictionary<TransformFactEnum, Transform> transforms = new();
        public static FactDictionary<GameObjectFactEnum, GameObject> gameobjects = new();
        public static FactDictionary<ComponentFactEnum, Component> components = new();
        public static FactDictionary<ComponentFactEnum, ISerializationCallbackReceiver> serializable = new();


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_gameFactsBoolean", booleans, typeof(FactDictionary<BooleanFactEnum, bool>));
            info.AddValue("m_gameFactsStrings", strings, typeof(FactDictionary<StringFactEnum, string>));
            info.AddValue("m_gameFactsInt", ints, typeof(FactDictionary<IntFactEnum, int>));
            info.AddValue("m_gameFactsFloat", floats, typeof(FactDictionary<FloatFactEnum, float>));
            info.AddValue("m_gameFactsTransform", transforms, typeof(FactDictionary<TransformFactEnum, Transform>));
            info.AddValue("m_gameObjects", gameobjects, typeof(FactDictionary<GameObjectFactEnum, GameObject>));
            info.AddValue("m_gameComponents", components, typeof(FactDictionary<ComponentFactEnum, Component>));
            info.AddValue("m_gameSerializable", serializable, typeof(FactDictionary<ComponentFactEnum, ISerializationCallbackReceiver>));
        }
        #endregion
    }
}
