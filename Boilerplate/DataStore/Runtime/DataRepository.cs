using System;

namespace DataStore.Runtime
{
    [Serializable]
    public sealed class DataRepository : FileDataRepository
    {
        public DataRepository(string package, string name) : base(GetDataPath(package, name))
        {
        }

        public static string GetDataPath(string Path, string name = "Data")
        {
            return $"{Path}/{name}.json";
        }
    }
}
