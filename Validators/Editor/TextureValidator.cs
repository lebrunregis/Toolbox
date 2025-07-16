using UnityEditor;
using UnityEngine;

namespace Validators.Editor
{
    public class TextureValidator : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (assetPath.Contains(" "))
            {
                Debug.Log($"The texture path is invalid, it contains a space : {assetPath}");
            }
        }
    }
}
