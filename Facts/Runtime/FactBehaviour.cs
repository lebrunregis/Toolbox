using System.Collections.Generic;

namespace Facts.Runtime
{
    public interface IFactBehaviour<U, T>
    {
        void CreateOrUpdateFact(U key, IFact<T> fact);
        bool TryGetFact(U key, out IFact<T> fact);
        bool RemoveFact(U key);

        protected Dictionary<U, IFact<T>> GameFacts();
    }
}
