using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager : MonoBehaviour
    {

        //user settings
        private static readonly UserDataRepository userDatastore = new();
        //save slots
        private static DataStore saveDatastore;
        //game data for save slots (optional)
        private static DataStore achievementStore;
        //game difficulty settings
        private static DataStore difficultyDatastore;

        private void OnEnable()
        {
            Debug.Log("Opening Data Stores");
            saveDatastore = new DataStore("Save", DataCategoryEnum.Save, "AutoSave");
            achievementStore = new DataStore("Achievements", DataCategoryEnum.Achievements, "Achievements");
            difficultyDatastore = new DataStore("Difficulty", DataCategoryEnum.Settings, "Easy");

        }

        public DataStore CurrentSaveSlot()
        {
            return saveDatastore;
        }

        private void OnDisable()
        {
            Debug.Log("Saving Data Stores");
            userDatastore.Save();
            saveDatastore.Save();
            achievementStore.Save();
            difficultyDatastore.Save();

        }
    }
}
