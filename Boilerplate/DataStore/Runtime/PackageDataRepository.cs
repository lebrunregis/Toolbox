using System;

namespace DataStore.Runtime
{
    [Serializable]
    public sealed class PackageDataRepository : FileDataRepository
    {
        public PackageDataRepository(string package, string name) : base(GetDataPath(package, name))
        {
        }

        public static string GetDataPath(string packageName, string name = "Data")
        {
            return string.Format("{0}/{1}/{2}.json", GameDataPath, packageName, name);
        }
    }
}
