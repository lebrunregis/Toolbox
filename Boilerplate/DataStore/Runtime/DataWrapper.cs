
using System;
using UnityEditor;
using UnityEngine;

namespace Facts.Runtime
{

    [Serializable]
    internal sealed class DataWrapper<T>
    {
#if PRETTY_PRINT_JSON
        const bool k_PrettyPrintJson = true;
#else
        private const bool k_PrettyPrintJson = false;
#endif

        [SerializeField]
        private T m_Value;

        public static string Serialize(T value)
        {
            var obj = new DataWrapper<T>() { m_Value = value };
            return EditorJsonUtility.ToJson(obj, k_PrettyPrintJson);
        }

        public static T Deserialize(string json)
        {
            var value = (object)Activator.CreateInstance<DataWrapper<T>>();
            EditorJsonUtility.FromJsonOverwrite(json, value);
            return ((DataWrapper<T>)value).m_Value;
        }

        public static T DeepCopy(T value)
        {
            if (typeof(ValueType).IsAssignableFrom(typeof(T)))
                return value;
            var str = Serialize(value);
            return Deserialize(str);
        }
    }

}