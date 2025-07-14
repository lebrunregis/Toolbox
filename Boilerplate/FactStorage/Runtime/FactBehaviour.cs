using System.Collections.Generic;

namespace SaveSystem.Runtime
{
    public interface IFactBehaviour<U, T>
    {
        void CreateFact(U key, IFact<T> fact);
        bool TryGetFact(U key, out IFact<T> fact);
        bool RemoveFact(U key);

        protected Dictionary<U, IFact<T>> GameFacts();
    }
}
