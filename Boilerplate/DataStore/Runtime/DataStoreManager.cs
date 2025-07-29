using DataStore.Data;
using UnityEngine;

namespace DataStore.Runtime
{
    public class DataStoreManager
    {

        //user settings
        private static readonly UserDataRepository userDataRepo = new();
        private static DataStore saveDataStore;
        private static DataStore userProfileDataStore;
        private static DataStore gameDataStore;
        private static DataStore runtimeDataStore;
        private static readonly DataCategoryEnum saveCategories = DataCategoryEnum.Save | DataCategoryEnum.Player;
        private static readonly DataCategoryEnum userCategories = DataCategoryEnum.Achievements | DataCategoryEnum.Player | DataCategoryEnum.Graphics | DataCategoryEnum.Audio;
        private static readonly DataCategoryEnum gameCategories = DataCategoryEnum.Difficulty;

        public static DataStore SaveDataStore
        {
            get
            {
                saveDataStore ??= new DataStore($"{Application.persistentDataPath}/{Application.productName}/Save", saveCategories);
                return saveDataStore;
            }
        }

        public static DataStore GameDataStore
        {
            get
            {
                gameDataStore ??= new DataStore($"{Application.dataPath}/_/{Application.productName}/GameData", gameCategories);
                return gameDataStore;
            }
        }

        public static DataStore RuntimeDataStore
        {
            get
            {
                runtimeDataStore ??= new DataStore("", DataCategoryEnum.Runtime);
                return runtimeDataStore;
            }
        }

        public static DataStore UserProfileDataStore
        {
            get
            {
                userProfileDataStore ??= new DataStore($"{Application.persistentDataPath}/{Application.productName}/UserData", userCategories);
                return userProfileDataStore;
            }
        }

        public static UserDataRepository UserDataRepo => userDataRepo;
    }

}
