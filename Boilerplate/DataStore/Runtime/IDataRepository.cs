using DataStore.Data;

namespace DataStore.Runtime
{
    public interface IDataRepository
    {
        string Name { get; }
        string Path { get; }
        DataScopeEnum Scope { get; }
        bool ContainsKey<T>(string key);
        T Get<T>(string key, T fallback);
        void Remove<T>(string key);
        void Save();
        void Set<T>(string key, T value);
        // T[] ToArray<T>();
    }
}
