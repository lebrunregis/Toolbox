namespace Platformization.Editor
{
    using System.Reflection;
    using UnityEditor;
    using UnityEditor.U2D;
    using UnityEngine;
    using UnityEngine.U2D;

    public class SpriteAtlasPreview : EditorWindow
    {
        private SpriteAtlas selectedSpriteAtlas;
        private Texture2D[] previewTextures;
        private string[] previewNames;
        private int selectedPreviewIndex;

        [MenuItem("Tools/Show Sprite Atlas Preview")]
        [MenuItem("Assets/Show Sprite Atlas Preview")]
        public static void ShowWindow()
        {
            GetWindow<SpriteAtlasPreview>("Sprite Atlas Preview");
        }
        [MenuItem("Tools/Show Sprite Atlas Preview", true)]
        [MenuItem("Assets/Show Sprite Atlas Preview", true)]
        public static bool _ShowWindow()
        {
            return Selection.activeObject is SpriteAtlas; // disable menu item if the selection is not a SpriteAtlas
        }

        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
        }
        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }
        private void OnDestroy()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        private bool triedPacking;
        private void OnEditorUpdate()
        {
            // check if selection changed
            if (Selection.activeObject != selectedSpriteAtlas)
            {
                selectedSpriteAtlas = Selection.activeObject as SpriteAtlas;
                selectedPreviewIndex = 0;
                previewNames = null;

                if (selectedSpriteAtlas != null)
                {
                    titleContent = new GUIContent("Sprite Atlas Preview - " + selectedSpriteAtlas.name);

                    // get sprite atlas preview using reflexion as the function is internal
                    var getPreviewTexturesMethod = typeof(SpriteAtlasExtensions).GetMethod("GetPreviewTextures", BindingFlags.Static | BindingFlags.NonPublic);
                    if (getPreviewTexturesMethod != null)
                    {
                        previewTextures = (Texture2D[])getPreviewTexturesMethod.Invoke(null, new object[] { selectedSpriteAtlas });
                    }

                    if ((previewTextures == null || previewTextures.Length == 0) && !triedPacking)
                    {
                        triedPacking = true;
                        // starting a pack preview unlock the display of the preview even if the spritePackerMode is set to BuildOnly,
                        // else the preview is maybe d'ont available because it needs to be packed ... so let's pack it!
                        SpriteAtlasUtility.PackAtlases(new[] { selectedSpriteAtlas }, EditorUserBuildSettings.activeBuildTarget);
                        SpriteAtlasUtility.CleanupAtlasPacking();
                        // set it to null to reenter the function and retry get the previews
                        selectedSpriteAtlas = null;
                    }

                    if (previewTextures is { Length: > 0 })
                    {
                        // create the values for the dropdown
                        previewNames = new string[previewTextures.Length];
                        for (var i = 0; i < previewTextures.Length; i++)
                            previewNames[i] = "MainTex - Page (" + (i + 1) + ")";
                    }
                }
                else
                {
                    titleContent = new GUIContent("Sprite Atlas Preview");
                    previewTextures = null;
                    selectedPreviewIndex = 0;
                }

                // force repaint
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedSpriteAtlas == null)
            {
                EditorGUILayout.LabelField("No Sprite Atlas selected.");
                return;
            }

            if (previewTextures == null || previewTextures.Length == 0)
            {
                EditorGUILayout.LabelField("No preview textures found.");

                if (GUILayout.Button("Pack Preview"))
                {
                    SpriteAtlasUtility.PackAtlases(new[] { selectedSpriteAtlas }, EditorUserBuildSettings.activeBuildTarget);
                    SpriteAtlasUtility.CleanupAtlasPacking();
                    selectedSpriteAtlas = null;
                }

                return;
            }

            // show the dropdown to select the texture to preview
            selectedPreviewIndex = EditorGUILayout.Popup($"Preview ({previewTextures.Length})", selectedPreviewIndex, previewNames);

            // display the selected preview
            var selectedPreview = previewTextures[selectedPreviewIndex];
            if (selectedPreview != null)
            {
                // calculate a zone to display the image with some padding
                var top = EditorGUIUtility.singleLineHeight * 2;
                var height = position.height - (EditorGUIUtility.singleLineHeight * 3);
                var left = EditorGUIUtility.singleLineHeight;
                var width = position.width - (EditorGUIUtility.singleLineHeight * 2);
                var rect = new Rect(left, top, width, height);

                // draw the texture with a checkboard behind
                EditorGUI.DrawTextureTransparent(rect, selectedPreview, ScaleMode.ScaleToFit);
            }
        }
    }
}
