using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Facts.Runtime
{
    [Serializable]
    public class GameManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        #region Publics
        public static FactDictionary<bool> m_gameFactsBoolean;
        public static FactDictionary<string> m_gameFactsStrings;
        public static FactDictionary<int> m_gameFactsInt;
        public static FactDictionary<float> m_gameFactsFloat;
        public static FactDictionary<ISerializationCallbackReceiver> m_gameFactsSerializable;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_gameFactsBoolean", m_gameFactsBoolean, typeof(FactDictionary<bool>));
            info.AddValue("m_gameFactsStrings", m_gameFactsStrings, typeof(FactDictionary<string>));
            info.AddValue("m_gameFactsInt", m_gameFactsInt, typeof(FactDictionary<int>));
            info.AddValue("m_gameFactsFloat", m_gameFactsFloat, typeof(FactDictionary<float>));
            info.AddValue("m_gameFactsSerializable", m_gameFactsSerializable, typeof(FactDictionary<ISerializationCallbackReceiver>));
        }


        public void Save(string fileName)
        {

        }

        public void Load(string fileName)
        {
            Debug.Log("Starting load");
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter bFormatter = new();
                m_gameFactsBoolean = bFormatter.Deserialize(stream) as FactDictionary<bool>;
                m_gameFactsStrings = bFormatter.Deserialize(stream) as FactDictionary<string>;
                m_gameFactsInt = bFormatter.Deserialize(stream) as FactDictionary<int>;
                m_gameFactsFloat = bFormatter.Deserialize(stream) as FactDictionary<float>;
                m_gameFactsSerializable = bFormatter.Deserialize(stream) as FactDictionary<ISerializationCallbackReceiver>;
            }
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {

        }
        #endregion

    }
}
