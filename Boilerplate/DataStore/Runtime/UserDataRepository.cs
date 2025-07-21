using DataStore.Data;
using Facts.Runtime;
using UnityEditor;

namespace DataStore.Runtime
{
    public class UserDataRepository
    {
        private static string GetEditorPrefKey<T>(string key)
        {
            return GetEditorPrefKey(typeof(T).FullName, key);
        }

        private static string GetEditorPrefKey(string fullName, string key)
        {
            return fullName + "::" + key;
        }

        private static void SetEditorPref<T>(string key, T value)
        {
            var k = GetEditorPrefKey<T>(key);

            if (typeof(T) == typeof(string))
                EditorPrefs.SetString(k, (string)(object)value);
            else if (typeof(T) == typeof(bool))
                EditorPrefs.SetBool(k, (bool)(object)value);
            else if (typeof(T) == typeof(float))
                EditorPrefs.SetFloat(k, (float)(object)value);
            else if (typeof(T) == typeof(int))
                EditorPrefs.SetInt(k, (int)(object)value);
            else
                EditorPrefs.SetString(k, DataWrapper<T>.Serialize(value));
        }

        private static T GetEditorPref<T>(string key, T fallback = default)
        {
            var k = GetEditorPrefKey<T>(key);

            if (!EditorPrefs.HasKey(k))
                return fallback;

            var o = (object)fallback;

            if (typeof(T) == typeof(string))
                o = EditorPrefs.GetString(k, (string)o);
            else if (typeof(T) == typeof(bool))
                o = EditorPrefs.GetBool(k, (bool)o);
            else if (typeof(T) == typeof(float))
                o = EditorPrefs.GetFloat(k, (float)o);
            else if (typeof(T) == typeof(int))
                o = EditorPrefs.GetInt(k, (int)o);
            else
                return DataWrapper<T>.Deserialize(EditorPrefs.GetString(k));

            return (T)o;
        }

        public DataScopeEnum Scope
        {
            get { return DataScopeEnum.User; }
        }

        public string Name
        {
            get { return "EditorPrefs"; }
        }

        public string Path
        {
            get { return string.Empty; }
        }


        public void Set<T>(string key, T value)
        {
            SetEditorPref<T>(key, value);
        }

        public T Get<T>(string key, T fallback = default)
        {
            return GetEditorPref<T>(key, fallback);
        }

        public bool ContainsKey<T>(string key)
        {
            return EditorPrefs.HasKey(GetEditorPrefKey<T>(key));
        }

        public void Remove<T>(string key)
        {
            EditorPrefs.DeleteKey(GetEditorPrefKey<T>(key));
        }
    }
}
