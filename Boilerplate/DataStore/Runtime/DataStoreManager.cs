using DebugBehaviour.Runtime;
using System;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager : VerboseMonoBehaviour
    {

        //user settings
        private static readonly UserDataRepository userDatastore = new();
        private static DataStore saveDataStore;
        private static DataStore userDataDataStore;
        public DataCategoryEnum saveCategories;
        public DataCategoryEnum userCategories;
        public bool saveOnClose = false;
        private void OnEnable()
        {
            Log("Opening Data Stores");
            saveDataStore = new DataStore($"{Application.persistentDataPath}/{Application.productName}/Save", saveCategories);
            userDataDataStore = new DataStore($"{Application.persistentDataPath}/{Application.productName}/UserData", userCategories);
            Log(saveDataStore.Get<string>(DataCategoryEnum.Player, "name", "Player not found"));
            saveDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
            userDataDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
        }


        private void OnDisable()
        {
            if (saveOnClose)
            {
                Log("Saving Data Store");
                saveDataStore.Save();
            }
        }
    }
}
