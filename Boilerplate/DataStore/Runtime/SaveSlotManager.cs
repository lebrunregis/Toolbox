using DataStore.Data;
using UnityEditor;
using UnityEngine;

namespace DataStore.Runtime
{
    public class SaveSlotManager:MonoBehaviour
    {
        private static readonly string[] saveRepositories = new[]
    {       "Autosave",
            "Save A",
            "Save B"
        };
        private static DataStore s_Settings;
        private int m_Repository;
        private void OnEnable()
        {
            s_Settings = new DataStore(new IDataRepository[]
            {
                new UserDataRepository(),
                new PackageDataRepository("Save", saveRepositories[0]),
                new PackageDataRepository("Save", saveRepositories[1]),
                new PackageDataRepository("Save", saveRepositories[2]),
            });

            m_Repository = s_Settings.Get<int>("ToolColorRepositoryName", DataScopeEnum.User);

           // m_ToolColor = GetToolColor(k_ProjectRepositories[m_Repository], Color.blue);
        }
    }
}
