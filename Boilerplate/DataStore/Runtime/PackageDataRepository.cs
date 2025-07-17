using System;

namespace DataStore.Runtime
{
    [Serializable]
    public sealed class PackageDataRepository : FileDataRepository
    {
        public PackageDataRepository(string package, string name) : base(GetSettingsPath(package, name))
        {
        }

        public static string GetSettingsPath(string packageName, string name = "Settings")
        {
            return string.Format("{0}/{1}/{2}.json", k_PackageDataDirectory, packageName, name);
        }
    }
}
