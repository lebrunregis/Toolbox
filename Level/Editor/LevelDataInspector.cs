using Level.Data;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[CustomEditor(typeof(LevelData))]
public class LevelDataInspector : Editor
{
    public LevelData levelData;
    void OnEnable()
    {

        // Setup the SerializedProperties.
        levelData = (LevelData)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Scene"))
        {
            AddScenePopup window = new(levelData);
            window.ShowUtility();
        }
        if (GUILayout.Button("Remove Scene"))
        {
            DeleteScenesPopup window = new(levelData);
            window.ShowUtility();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }

    private class AddScenePopup : EditorWindow
    {
        public LevelData levelData;
        TextField nameField;

        public AddScenePopup(LevelData levelData)
        {
            this.levelData = levelData;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy
            Label label = new("Enter new Scene name:");
            root.Add(label);

            nameField = new();
            root.Add(nameField);

            // Create button
            Button button = new()
            {
                name = "add",
                text = "Add"
            };

            button.clicked += ButtonClickedCallback;
            root.Add(button);
        }

        void ButtonClickedCallback()
        {
            string text = nameField.text;
            if (!string.IsNullOrEmpty(text))
            {
                string sceneName = text[..1].ToUpper() + text[1..];
                string path = AssetDatabase.GetAssetPath(levelData.GetInstanceID());
                string directory = Path.GetDirectoryName(path);
                Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

                string filepath = directory + "/Scenes/" + levelData.name + "-" + sceneName + ".unity";
                if (EditorSceneManager.SaveScene(newScene, filepath))
                {
                    string guid = AssetDatabase.AssetPathToGUID(filepath);
                    AssetReference assetReference = new AssetReference(guid);
                    //  levelData.assetReferences  = levelData.assetReferences.Append(assetReference).ToArray();
                    levelData.ReloadLevel();
                }
            }
        }
    }

    private class DeleteScenesPopup : EditorWindow
    {
        LevelData levelData;
        private readonly Toggle[] ToDelete;
        Vector2 scrollPos;
        public DeleteScenesPopup(LevelData levelData)
        {
            this.levelData = levelData;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy
            Label label = new("Select scenes to delete");
            root.Add(label);
            EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));
            foreach (AssetReference asset in levelData.assetReferences)
            {
                GUILayout.BeginHorizontal();
                // Create button
                Label sceneLabel = new()
                {
                    name = "Label" + asset.ToString(),
                    text = asset.ToString(),
                };
                root.Add(sceneLabel);

                // Create toggle
                Toggle toggle = new()
                {
                    name = "Toggle" + asset.ToString(),
                };
                root.Add(toggle);
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            // Create button
            Button button = new()
            {
                name = "remove",
                text = "Remove"
            };
            button.clicked += DeleteSelectedScenes;
            root.Add(button);
        }

        public void DeleteSelectedScenes()
        {
            Debug.Log("scenes deleted");
            levelData.ReloadLevel();
        }
    }
}

