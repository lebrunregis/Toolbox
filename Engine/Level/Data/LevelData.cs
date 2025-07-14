using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Level.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Game/Level")]
    public class LevelData : ScriptableObject
    {

        [SerializeField]
        public Scene[] openedScenes;

        #region Publics
        public AssetReference[] assetReferences;
        public AssetReference activeSceneForLighting;
        #endregion

        #region Public API
        public void LoadLevel()
        {
            for (int i = 0; i < assetReferences.Length; i++)
            {
                AssetReference sceneReference = assetReferences[i];
                if (!IsSceneValid(sceneReference))
                {
                    AsyncOperationHandle<Scene> asyncOperationHandleScene = Addressables.LoadAssetAsync<Scene>(sceneReference);
                }
                else
                {
                    Debug.LogError("Scene Adressable not found");
                }
            }
        }

        public void OpenEditorLevel()
        {
            List<Scene> scenes = new();
            for (int i = 0; i < assetReferences.Length; i++)
            {
                AssetReference sceneReference = assetReferences[i];

                if (IsSceneValid(sceneReference))
                {
                    Scene scene = Addressables.LoadAssetAsync<Scene>(sceneReference).Result;
                    scenes.Add(scene);
                }
                else
                {
                    Debug.LogError("Scene Adressable not found");
                }
            }
            openedScenes = scenes.ToArray();

            if (IsSceneValid(activeSceneForLighting))
            {
                Scene lightningScene = Addressables.LoadAssetAsync<Scene>(activeSceneForLighting).Result;
                SceneManager.SetActiveScene(lightningScene);
            }
            else
            {
                Debug.Log("Could not find lighting scene. Think about adding one!");
            }

        }

        public void CloseEditorLevel()
        {
            for (int i = openedScenes.Length - 1; i > 0; i--)
            {
                Addressables.Release(openedScenes[i]);
            }
        }

        public void ReloadLevel()
        {
            CloseEditorLevel();
            OpenEditorLevel();
        }
        #endregion

        public static bool IsSceneValid(AssetReference sceneReference)
        {
            return string.IsNullOrEmpty(sceneReference.AssetGUID);
        }
    }
}
