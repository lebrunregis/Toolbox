using System.Collections.Generic;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager : MonoBehaviour
    {

        //user settings
        private static readonly UserDataRepository userDatastore = new();
        private static DataStore test;
        private readonly List<KeyValuePair<DataCategoryEnum, string>> repoList = new()
{
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.Audio, "Audio"),
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.Achievements, "Achievements"),
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.Difficulty, "Difficulty"),
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.Save, "Save"),
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.Graphics,"Graphics"),
    new KeyValuePair<DataCategoryEnum, string>(DataCategoryEnum.GameState,"GameState"),
};
        private void OnEnable()
        {
            Debug.Log("Opening Data Stores");
            test = new DataStore($"{Application.dataPath}/Save", repoList);
        }


        private void OnDisable()
        {
            Debug.Log("Saving Data Store");
            test.Save();

        }
    }
}
