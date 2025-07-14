using System.Collections.Generic;

namespace SaveSystem.Runtime
{
    public class FactDictionary<U, T> : IFactBehaviour<U, T>
    {
        #region Publics
        public Dictionary<U, IFact<T>> _facts = new();

        public void CreateFact(U key, IFact<T> fact)
        {
            _facts.Add(key, fact);
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

        public void Clear()
        {
            _facts.Clear();
        }
        #endregion
    }
}
