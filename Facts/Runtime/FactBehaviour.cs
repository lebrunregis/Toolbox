using System.Collections.Generic;

namespace Facts.Runtime
{
    public interface IFactBehaviour<T>
    {
        void CreateOrUpdateFact(string key, IFact<T> fact);
        bool TryGetFact(string key, out IFact<T> fact);
        bool RemoveFact(string key);

        protected Dictionary<string, IFact<T>> GameFacts();
    }
}
