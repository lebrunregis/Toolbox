using DebugBehaviour.Runtime;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager : VerboseMonoBehaviour
    {

        //user settings
        private static readonly UserDataRepository userDatastore = new();
        private static DataStore saveDataStore;
        private static DataStore userProfileDataStore;
        private static DataStore gameDataStore;
        public DataCategoryEnum saveCategories = DataCategoryEnum.Save | DataCategoryEnum.Player;
        public DataCategoryEnum userCategories = DataCategoryEnum.Achievements | DataCategoryEnum.Player | DataCategoryEnum.Graphics | DataCategoryEnum.Audio;
        public DataCategoryEnum gameCategories = DataCategoryEnum.Difficulty;
        public bool saveOnClose = false;
        private void OnEnable()
        {
            Log("Opening Data Stores");
            saveDataStore = new DataStore($"{Application.persistentDataPath}/{Application.productName}/Save", saveCategories);
            userProfileDataStore = new DataStore($"{Application.persistentDataPath}/{Application.productName}/UserData", userCategories);
            gameDataStore = new DataStore($"{Application.dataPath}/_/{Application.productName}/GameData", gameCategories);
            Log(saveDataStore.Get<string>(DataCategoryEnum.Player, "name", "Player not found"));
            saveDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
            userProfileDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
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
