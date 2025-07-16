using UnityEditor;

namespace Validators.Editor
{
    // [InitializeOnLoad]
    public static class PlaymodeBlocker
    {
        private static void BlockPlaymode()
        {
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }
        private
           static void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingEditMode)
            {
                string[] guids = AssetDatabase.FindAssets("t:Texture");
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    if (path.Contains(" "))
                    {
                        EditorApplication.isPlaying = false;
                    }
                }
            }
        }
    }
}
