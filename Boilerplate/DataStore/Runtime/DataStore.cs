using DataStore.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStore
    {
        private readonly Dictionary<DataCategoryEnum, IDataRepository> repositories = new();
        public event Action BeforeSettingsSaved;
        public event Action AfterSettingsSaved;

        public DataStore(string path, DataCategoryEnum repos)
        {
            foreach (DataCategoryEnum flag in Enum.GetValues(typeof(DataCategoryEnum)))
            {
                if (repos.HasFlag(flag))
                {
                    repositories.Add(flag, new DataRepository(path, Enum.GetName(typeof(DataCategoryEnum), flag)));
                }
            }
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

        public void Set<T>(DataCategoryEnum category, string key, T value)
        {
            if (repositories.TryGetValue(category, out IDataRepository repository))
            {
                repository.Set(key, value);
            }
            else
            {
                Debug.LogError("Trying to save to an uninitialized save repository!");
            }
        }

        public T Get<T>(DataCategoryEnum category, string key, T fallback = default)
        {
            if (repositories.TryGetValue(category, out IDataRepository repository))
            {
                return repository.Get<T>(key, fallback);
            }
            else
            {
                Debug.LogError("Trying to get from an uninitialized save repository!");
                return fallback;
            }

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

