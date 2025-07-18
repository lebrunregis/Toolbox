using System;
using System.Collections.Generic;
using System.Linq;
using DataStore.Data;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStore
    {
        private readonly Dictionary<DataCategoryEnum, IDataRepository> repositories = new();
        public event Action BeforeSettingsSaved;
        public event Action AfterSettingsSaved;

        private DataStore()
        {
        }

        public DataStore(string package, DataCategoryEnum category = DataCategoryEnum.None, string dataFileName = "Data")
        {
            repositories.Add(category, new DataRepository(package, dataFileName));

        }
        public DataStore(IEnumerable<KeyValuePair<DataCategoryEnum, IDataRepository>> repo)
        {

            repositories = repo.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IDataRepository GetRepository(DataCategoryEnum scope)
        {
            repositories.TryGetValue(scope, out IDataRepository repository);
            return repository;
        }


        public IDataRepository GetRepository(DataScopeEnum scope, string name)
        {
            foreach (IDataRepository repo in repositories.Values)
                if (repo.Scope == scope && string.Equals(repo.Name, name))
                    return repo;
            return null;
        }

        public void Save()
        {
            BeforeSettingsSaved?.Invoke();

            foreach (IDataRepository repo in repositories.Values)
                repo.Save();

            AfterSettingsSaved?.Invoke();
        }

        public void Set<T>(string key, T value, DataScopeEnum scope = DataScopeEnum.Project)
        {

            Set<T, DataRepository>(key, value);

        }

        public void Set<T>(string key, T value, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {

            Set<T, DataRepository>(key, value, repositoryName);

        }

        public void Set<T, K>(string key, T value, string repositoryName = null) where K : IDataRepository
        {
            bool foundScopeRepository = false;

            foreach (IDataRepository repo in repositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repo.Name == repositoryName))
                {
                    repo.Set<T>(key, value);
                    foundScopeRepository = true;
                }
            }

            if (!foundScopeRepository)
                Debug.LogWarning($"No repository with type {typeof(K)} found.");
        }

        public T Get<T>(string key, DataScopeEnum scope = DataScopeEnum.Project, T fallback = default)
        {

            return Get<T, DataRepository>(key, fallback);

        }

        public T Get<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project, T fallback = default)
        {

            return Get<T, DataRepository>(key, fallback, repositoryName);

        }

        public T Get<T, K>(string key, T fallback = default, string repositoryName = null) where K : IDataRepository
        {
            foreach (IDataRepository repo in repositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repo.Name == repositoryName))
                    return repo.Get<T>(key, fallback);
            }

            Debug.LogWarning($"No repository with type {typeof(K)} found.");
            return fallback;
        }

        public bool ContainsKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {

            return ContainsKey<T, DataRepository>(key);

        }

        public bool ContainsKey<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {

            return ContainsKey<T, DataRepository>(key, repositoryName);

        }

        public bool ContainsKey<T, K>(string key, string repositoryName = null) where K : IDataRepository
        {
            foreach (IDataRepository repo in repositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repositoryName == repo.Name))
                    return repo.ContainsKey<T>(key);
            }

            Debug.LogWarning($"No repository with type {typeof(K)} found.");
            return false;
        }

        public void DeleteKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {

            DeleteKey<T, DataRepository>(key);

        }

        public void DeleteKey<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {

            DeleteKey<T, DataRepository>(key, repositoryName);

        }

        public void DeleteKey<T, K>(string key, string repositoryName = null) where K : IDataRepository
        {
            bool foundScopeRepository = false;

            foreach (IDataRepository repo in repositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repositoryName == repo.Name))
                {
                    foundScopeRepository = true;
                    repo.Remove<T>(key);
                }
            }

            if (!foundScopeRepository)
                Debug.LogWarning($"No repository with type {typeof(K)} found.");
        }
    }
}

