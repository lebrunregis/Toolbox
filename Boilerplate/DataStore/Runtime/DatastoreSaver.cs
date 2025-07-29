using DataStore.Data;
using DebugBehaviour.Runtime;

namespace DataStore.Runtime
{
    public class DatastoreSaver : VerboseMonoBehaviour
    {
        public bool saveOnClose = false;
        private void OnDisable()
        {
            if (saveOnClose)
            {
                DataStoreManager.SaveDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
                DataStoreManager.UserProfileDataStore.Set<string>(DataCategoryEnum.Player, "name", "Player");
                Log("Saving Data Store");
                DataStoreManager.SaveDataStore.Save();
            }
        }
    }
}
