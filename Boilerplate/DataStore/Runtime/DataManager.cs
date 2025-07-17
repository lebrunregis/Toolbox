using DataStore.Data;

namespace DataStore.Runtime
{
    public class DataManager
    {

        internal const string savePath = "save";

        private static DataStore s_Instance;

        internal static DataStore Instance
        {
            get
            {
                s_Instance ??= new DataStore(savePath);

                return s_Instance;
            }
        }

        public static void Save()
        {
            Instance.Save();
        }

        public static T Get<T>(string key, DataScopeEnum scope = DataScopeEnum.Project, T fallback = default)
        {
            return Instance.Get<T>(key, scope, fallback);
        }

        public static void Set<T>(string key, T value, DataScopeEnum scope = DataScopeEnum.Project)
        {
            Instance.Set<T>(key, value, scope);
        }

        public static bool ContainsKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {
            return Instance.ContainsKey<T>(key, scope);
        }
    }
}
