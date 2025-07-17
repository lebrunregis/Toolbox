using System;
using System.Collections.Generic;
using DataStore.Data;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStore
    {
        private Dictionary<DataScopeEnum, IDataRepository> m_dataRepositories;
        public event Action BeforeSettingsSaved;
        public event Action AfterSettingsSaved;

        private DataStore()
        {
        }

        public DataStore(string package, string dataFileName = "Data")
        {
            m_dataRepositories.Add(DataScopeEnum.Project, new PackageDataRepository(package, dataFileName));
            m_dataRepositories.Add(DataScopeEnum.User, new UserDataRepository());
        }
        public DataStore(IEnumerable<IDataRepository> repositories)
        {
            // m_dataRepositories = repositories.ToArray();
        }

        public IDataRepository GetRepository(DataScopeEnum scope)
        {
            m_dataRepositories.TryGetValue(scope, out var repository);
            return repository;
        }


        public IDataRepository GetRepository(DataScopeEnum scope, string name)
        {
            foreach (var repo in m_dataRepositories.Values)
                if (repo.Scope == scope && string.Equals(repo.Name, name))
                    return repo;
            return null;
        }

        public void Save()
        {
            BeforeSettingsSaved?.Invoke();

            foreach (var repo in m_dataRepositories.Values)
                repo.Save();

            AfterSettingsSaved?.Invoke();
        }

        public void Set<T>(string key, T value, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                Set<T, PackageDataRepository>(key, value);
            Set<T, UserDataRepository>(key, value);
        }

        public void Set<T>(string key, T value, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                Set<T, PackageDataRepository>(key, value, repositoryName);
            Set<T, UserDataRepository>(key, value, repositoryName);
        }

        public void Set<T, K>(string key, T value, string repositoryName = null) where K : IDataRepository
        {
            bool foundScopeRepository = false;

            foreach (var repo in m_dataRepositories.Values)
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
            if (scope == DataScopeEnum.Project)
                return Get<T, PackageDataRepository>(key, fallback);
            return Get<T, UserDataRepository>(key, fallback);
        }

        public T Get<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project, T fallback = default)
        {
            if (scope == DataScopeEnum.Project)
                return Get<T, PackageDataRepository>(key, fallback, repositoryName);
            return Get<T, UserDataRepository>(key, fallback, repositoryName);
        }

        public T Get<T, K>(string key, T fallback = default, string repositoryName = null) where K : IDataRepository
        {
            foreach (var repo in m_dataRepositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repo.Name == repositoryName))
                    return repo.Get<T>(key, fallback);
            }

            Debug.LogWarning($"No repository with type {typeof(K)} found.");
            return fallback;
        }

        public bool ContainsKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                return ContainsKey<T, PackageDataRepository>(key);
            return ContainsKey<T, UserDataRepository>(key);
        }

        public bool ContainsKey<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                return ContainsKey<T, PackageDataRepository>(key, repositoryName);
            return ContainsKey<T, UserDataRepository>(key, repositoryName);
        }

        public bool ContainsKey<T, K>(string key, string repositoryName = null) where K : IDataRepository
        {
            foreach (var repo in m_dataRepositories.Values)
            {
                if (repo is K && (string.IsNullOrEmpty(repositoryName) || repositoryName == repo.Name))
                    return repo.ContainsKey<T>(key);
            }

            Debug.LogWarning($"No repository with type {typeof(K)} found.");
            return false;
        }

        public void DeleteKey<T>(string key, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                DeleteKey<T, PackageDataRepository>(key);
            DeleteKey<T, UserDataRepository>(key);
        }

        public void DeleteKey<T>(string key, string repositoryName, DataScopeEnum scope = DataScopeEnum.Project)
        {
            if (scope == DataScopeEnum.Project)
                DeleteKey<T, PackageDataRepository>(key, repositoryName);
            DeleteKey<T, UserDataRepository>(key, repositoryName);
        }

        public void DeleteKey<T, K>(string key, string repositoryName = null) where K : IDataRepository
        {
            bool foundScopeRepository = false;

            foreach (var repo in m_dataRepositories.Values)
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

