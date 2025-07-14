using System.Collections.Generic;

namespace SaveSystem.Runtime
{
    public class FactList<U, T>
    {

        public List<KeyValuePair<U, IFact<T>>> _facts = new();

    }
}
