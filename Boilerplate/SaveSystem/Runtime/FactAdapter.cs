using Facts.Runtime;

namespace SaveSystem.Runtime
{
    public static class FactAdapter<U, T>
    {
        public static void ToList(FactDictionary<U, T> factDictionary, FactList<U, T> factList)
        {
            var e = factDictionary._facts.GetEnumerator();
            while (e.MoveNext())
            {
                factList._facts.Add(e.Current);
            }
        }

        public static void ToDictionary(FactList<U, T> facts, FactDictionary<U, T> factDictionary)
        {
            var e = facts._facts.GetEnumerator();
            while (e.MoveNext())
            {
                factDictionary._facts.Add(e.Current.Key, e.Current.Value);
            }
        }
    }

}