using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facts.Runtime
{
    public class FactDictionary<U, T> : IFactBehaviour<U, T>, ISerializationCallbackReceiver
    {
        #region Publics
        public Dictionary<U, IFact<T>> _facts = new();
        public List<U> _keys = new();
        public List<IFact<T>> _values = new();

        public void CreateOrUpdateFact(U key, IFact<T> fact)
        {
            _facts.Add(key, fact);
        }

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();
            // For each key/value pair in the dictionary, add the key to the keys list and the value to the values list
            foreach (var kvp in _facts)
            {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _facts = new Dictionary<U, IFact<T>>();
            // Loop through the list of keys and values and add each key/value pair to the dictionary
            for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
                _facts.Add(_keys[i], _values[i]);
        }

        public bool RemoveFact(U key)
        {
            return _facts.Remove(key);
        }

        public bool TryGetFact(U key, out IFact<T> fact)
        {
            return _facts.TryGetValue(key, out fact);
        }

        Dictionary<U, IFact<T>> IFactBehaviour<U, T>.GameFacts()
        {
            return _facts;
        }
        #endregion
    }
}
