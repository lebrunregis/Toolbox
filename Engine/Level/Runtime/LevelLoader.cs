using Level.Data;
using UnityEngine;

namespace Level.Runtime
{
    public class LevelLoader : DebugBehaviour.Runtime.VerboseMonoBehaviour
    {
        #region Unity API
        private void Awake()
        {
            if (levelData != null)
            {
                levelData.LoadLevel();
            }

        }
        #endregion

        #region Private and protectd

        [SerializeField]
        private LevelData levelData;
        #endregion
    }
}

