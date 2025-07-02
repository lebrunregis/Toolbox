using System.Collections.Generic;

namespace Facts.Runtime
{
    public class FactDictionary<T> : IFactBehaviour<T>
    {
        #region Publics
        public Dictionary<string, IFact<T>> _facts;

        public void CreateOrUpdateFact(string key, IFact<T> fact)
        {
            _facts.Add(key, fact);
        }

        public bool RemoveFact(string key)
        {
            return _facts.Remove(key);
        }

        public bool TryGetFact(string key, out IFact<T> fact)
        {
            return _facts.TryGetValue(key, out fact);
        }

        Dictionary<string, IFact<T>> IFactBehaviour<T>.GameFacts()
        {
            return _facts;
        }
        #endregion
    }
}
