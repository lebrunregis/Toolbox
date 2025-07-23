using DataStore.Data;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DataStore.Runtime
{
    [Serializable]
    public class FileDataRepository : IDataRepository
    {

        private const bool k_PrettyPrintJson = true;

        private bool initialized;
        private readonly string path;
        [SerializeField]
        private DataDictionary dictionary = new();
        private Hash128 jsonHash;
        public FileDataRepository(string path)
        {
            this.path = path;
            initialized = false;
            AssemblyReloadEvents.beforeAssemblyReload += Save;
            EditorApplication.quitting += Save;
        }

        private void Init()
        {
            if (initialized)
                return;

            initialized = true;

            if (TryLoadSavedJson(out string json))
            {
                dictionary = null;
                jsonHash = Hash128.Compute(json);
                EditorJsonUtility.FromJsonOverwrite(json, this);
            }

            dictionary ??= new DataDictionary();
        }

        public virtual DataScopeEnum Scope => DataScopeEnum.Project;

        public string Path
        {
            get { return path; }
        }

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public bool TryLoadSavedJson(out string json)
        {
            bool fileFound = false;
            json = string.Empty;
            if (File.Exists(Path))
            {
                json = File.ReadAllText(Path);
                fileFound = true;
            }
            return fileFound;
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
            if (jsonHash == Hash128.Compute(json)
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
                jsonHash = Hash128.Compute(json);
                File.WriteAllText(Path, json);
                Debug.Log("Writing " + Path);
            }
            catch (UnauthorizedAccessException)
            {
                Debug.LogWarning($"Could not save package settings to {Path}");
            }
        }

        public void Set<T>(string key, T value)
        {
            Init();
            dictionary.Set<T>(key, value);
        }

        public T Get<T>(string key, T fallback = default)
        {
            Init();
            return dictionary.Get<T>(key, fallback);
        }

        public bool ContainsKey<T>(string key)
        {
            Init();
            return dictionary.ContainsKey<T>(key);
        }

        public void Remove<T>(string key)
        {
            Init();
            dictionary.Remove<T>(key);
        }
    }
}

