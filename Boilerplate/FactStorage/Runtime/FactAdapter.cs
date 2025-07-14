using System.Collections.Generic;
using Facts.Runtime;

namespace SaveSystem.Runtime
{
    public static class FactAdapter
    {
        public static void ToList<U, T>(FactDictionary<U, T> factDictionary, LinkedList<KeyValuePair<U, T>> factList)
        {
            Dictionary<U, IFact<T>>.Enumerator enumerator = factDictionary._facts.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Value.PersistanceState == Facts.Data.FactPersistenceEnum.Serialized)
                {
                    KeyValuePair<U, T> node = new(enumerator.Current.Key, enumerator.Current.Value.Value());
                    factList.AddLast(node);
                }
            }
        }

        public static void ToDictionary<T1, T2>(LinkedList<KeyValuePair<T1, T2>> factsList, FactDictionary<T1, T2> factDictionary)
        {
            var e = factsList.GetEnumerator();
            while (e.MoveNext())
            {
                factDictionary._facts.Add(e.Current.Key, new Fact<T2>(e.Current.Value, Facts.Data.FactPersistenceEnum.Serialized));
            }
        }
    }

}