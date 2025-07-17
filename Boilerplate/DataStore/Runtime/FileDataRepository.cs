using System;
using System.IO;
using DataStore.Data;
using UnityEditor;
using UnityEngine;

namespace DataStore.Runtime
{
    [Serializable]
    public class FileDataRepository : IDataRepository
    {

        protected const string GameDataPath = "ProjectData/Packages";
        protected const string UserDataPath = "UserData/Packages";

        private const bool k_PrettyPrintJson = true;

        private bool m_Initialized;
        private readonly string m_Path;
        [SerializeField]
        private DataDictionary m_Dictionary = new();
        private Hash128 m_JsonHash;
        public FileDataRepository(string path)
        {
            m_Path = path;
            m_Initialized = false;
            AssemblyReloadEvents.beforeAssemblyReload += Save;
            EditorApplication.quitting += Save;
        }

        private void Init()
        {
            if (m_Initialized)
                return;

            m_Initialized = true;

            if (TryLoadSavedJson(out string json))
            {
                m_Dictionary = null;
                m_JsonHash = Hash128.Compute(json);
                EditorJsonUtility.FromJsonOverwrite(json, this);
            }

            m_Dictionary ??= new DataDictionary();
        }

        public virtual DataScopeEnum Scope => DataScopeEnum.Project;

        public string Path
        {
            get { return m_Path; }
        }

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public bool TryLoadSavedJson(out string json)
        {
            json = string.Empty;
            if (!File.Exists(Path))
                return false;
            json = File.ReadAllText(Path);
            return true;
        }
        public void Save()
        {
            Init();

            if (!File.Exists(Path))
            {
                var directory = System.IO.Path.GetDirectoryName(Path);

                if (string.IsNullOrEmpty(directory))
                {
                    Debug.LogError(
                        $"Settings file {Name} is saved to an invalid path: {Path}. Settings will not be saved.");
                    return;
                }

                Directory.CreateDirectory(directory);
            }

            string json = EditorJsonUtility.ToJson(this, k_PrettyPrintJson);

            // While unlikely, a hash collision is possible. Always test the actual saved contents before early exit.
            if (m_JsonHash == Hash128.Compute(json)
                && TryLoadSavedJson(out string existing)
                && existing.Equals(json))
                return;

#if UNITY_2019_3_OR_NEWER
            // AssetDatabase.IsOpenForEdit can be a very slow synchronous blocking call when Unity is connected to
            // Perforce Version Control. Especially if it's called repeatedly with every EditorGUI redraw.
            if (File.Exists(Path) && !AssetDatabase.IsOpenForEdit(Path))
            {
                if (!AssetDatabase.MakeEditable(Path))
                {
                    Debug.LogWarning($"Could not save package settings to {Path}");
                    return;
                }
            }
#endif

            try
            {
                m_JsonHash = Hash128.Compute(json);
                File.WriteAllText(Path, json);
            }
            catch (UnauthorizedAccessException)
            {
                Debug.LogWarning($"Could not save package settings to {Path}");
            }
        }

        public void Set<T>(string key, T value)
        {
            Init();
            m_Dictionary.Set<T>(key, value);
        }

        public T Get<T>(string key, T fallback = default)
        {
            Init();
            return m_Dictionary.Get<T>(key, fallback);
        }

        public bool ContainsKey<T>(string key)
        {
            Init();
            return m_Dictionary.ContainsKey<T>(key);
        }

        public void Remove<T>(string key)
        {
            Init();
            m_Dictionary.Remove<T>(key);
        }
    }
}

