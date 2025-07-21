using Facts.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataStore.Runtime
{
    [Serializable]
    public class DataDictionary : ISerializationCallbackReceiver
    {
        [Serializable]
        private struct DataKeyValuePair
        {
            public string type;
            public string key;
            public string value;
        }

        [SerializeField]
        private List<DataKeyValuePair> m_DictionaryValues = new();

        internal Dictionary<Type, Dictionary<string, string>> dictionary = new();

        public bool ContainsKey<T>(string key)
        {
            return dictionary.ContainsKey(typeof(T)) && dictionary[typeof(T)].ContainsKey(key);
        }

        public void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            var type = typeof(T).AssemblyQualifiedName;

            SetJson(type, key, DataWrapper<T>.Serialize(value));
        }

        internal void SetJson(string type, string key, string value)
        {
            var typeValue = Type.GetType(type);

            if (typeValue != null)
            {
                if (!dictionary.TryGetValue(typeValue, out Dictionary<string, string> entries))
                    dictionary.Add(typeValue, entries = new Dictionary<string, string>());

                if (entries.ContainsKey(key))
                    entries[key] = value;
                else
                    entries.Add(key, value);
            }
            else
            {
                throw new ArgumentException("\"type\" must be an assembly qualified type name.");
            }
        }

        public T Get<T>(string key, T fallback = default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");


            if (dictionary.TryGetValue(typeof(T), out Dictionary<string, string> entries) && entries.ContainsKey(key))
            {
                try
                {
                    return DataWrapper<T>.Deserialize(entries[key]);
                }
                catch
                {
                    return fallback;
                }
            }

            return fallback;
        }

        public void Remove<T>(string key)
        {

            if (!dictionary.TryGetValue(typeof(T), out Dictionary<string, string> entries) || !entries.ContainsKey(key))
                return;

            entries.Remove(key);
        }

        public void OnBeforeSerialize()
        {
            if (m_DictionaryValues == null)
                return;

            m_DictionaryValues.Clear();

            foreach (var type in dictionary)
            {
                foreach (var entry in type.Value)
                {
                    m_DictionaryValues.Add(new DataKeyValuePair()
                    {
                        type = type.Key.AssemblyQualifiedName,
                        key = entry.Key,
                        value = entry.Value
                    });
                }
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary.Clear();

            foreach (var entry in m_DictionaryValues)
            {

                var type = Type.GetType(entry.type);

                if (type == null)
                {
                    UnityEngine.Debug.LogWarning("Could not instantiate type \"" + entry.type + "\". Skipping key: " + entry.key + ".");
                    continue;
                }

                if (dictionary.TryGetValue(type, out Dictionary<string, string> entries))
                    entries.Add(entry.key, entry.value);
                else
                    dictionary.Add(type, new Dictionary<string, string>() { { entry.key, entry.value } });
            }
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            foreach (var type in dictionary)
            {
                sb.AppendLine("Type: " + type.Key);

                foreach (var entry in type.Value)
                {
                    sb.AppendLine(string.Format("   {0,-64}{1}", entry.Key, entry.Value));
                }
            }

            return sb.ToString();
        }
    }
}
