using DataStore.Data;

namespace DataStore.Runtime
{
    public class DataManager
    {
        private static string _savePath;
        private static DataCategoryEnum _repos;
        private static DataStore s_Instance;

        public DataManager(string savePath, DataCategoryEnum repos)
        {
            _savePath = savePath;
            _repos = repos;

        }


        internal static DataStore Instance
        {
            get
            {
                s_Instance ??= new DataStore(_savePath, _repos);

                return s_Instance;
            }
        }

        public static void Save()
        {
            Instance.Save();
        }

        public static T Get<T>(DataCategoryEnum category, string key, T fallback = default)
        {
            return Instance.Get<T>(category, key, fallback);
        }

        public static void Set<T>(DataCategoryEnum category, string key, T value)
        {
            Instance.Set<T>(category, key, value);
        }

        public static bool ContainsKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {
            return Instance.ContainsKey<T>(key, scope);
        }
    }
}
