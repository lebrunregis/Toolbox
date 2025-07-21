using DebugBehaviour.Runtime;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager : VerboseMonoBehaviour
    {

        //user settings
        private static readonly UserDataRepository userDatastore = new();
        private static DataStore saveDataStore;
        public DataCategoryEnum saveCategories;
        public DataCategoryEnum userCategories;
        private void OnEnable()
        {
            Log("Opening Data Stores");
            saveDataStore = new DataStore($"{Application.dataPath}/Save", saveCategories);
            saveDataStore = new DataStore($"{Application.dataPath}/UserData", userCategories);
            saveDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
        }


        private void OnDisable()
        {
            Log("Saving Data Store");
            saveDataStore.Save();

        }
    }
}
