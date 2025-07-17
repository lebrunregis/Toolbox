using UnityEditor;

namespace DataStore.Runtime
{
    public class DataManager
    {

        internal const string k_PackageName = "game.unity.savesystem";

        private static DataStore s_Instance;

        internal static DataStore Instance
        {
            get
            {
                s_Instance ??= new DataStore(k_PackageName);

                return s_Instance;
            }
        }

        public static void Save()
        {
            Instance.Save();
        }

        public static T Get<T>(string key, SettingsScope scope = SettingsScope.Project, T fallback = default(T))
        {
            return Instance.Get<T>(key, scope, fallback);
        }

        public static void Set<T>(string key, T value, SettingsScope scope = SettingsScope.Project)
        {
            Instance.Set<T>(key, value, scope);
        }

        public static bool ContainsKey<T>(string key, SettingsScope scope = SettingsScope.Project)
        {
            return Instance.ContainsKey<T>(key, scope);
        }
    }
}
