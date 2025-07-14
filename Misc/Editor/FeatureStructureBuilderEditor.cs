using System.IO;
using UnityEditor;
using UnityEngine;

public class FeatureFolderStructureBuilderEditor : EditorWindow
{

    public string editorWindowText = "Choose a feature name: ";

    public string dataPath = Application.dataPath;
    public string featureStorePath = AssetDatabase.GetAssetPath(Selection.activeObject);
    private string featureName = "";
    private bool makeData = true;
    private bool makeEditor = true;
    private bool makeRuntime = true;

    [MenuItem("Assets/Create/Scripting/Stuctured Assembly Definition", priority = 11)]
    private static void CreateStructure()
    {
        FeatureFolderStructureBuilderEditor window = ScriptableObject.CreateInstance<FeatureFolderStructureBuilderEditor>();
        window.ShowUtility();
    }

    [System.Serializable]
    public class AssemblyDefinition
    {
        public string name;
        public string rootNamespace;
        public string[] references;
        public string[] includePlatforms;
    }


    private void OnGUI()
    {
        bool enterPressed = false;

        EditorGUILayout.LabelField(editorWindowText);
        GUI.SetNextControlName("FeatureName");
        featureName = EditorGUILayout.TextField(featureName);
        makeData = EditorGUILayout.Toggle("Make Data", makeData);
        makeEditor = EditorGUILayout.Toggle("Make Editor", makeEditor);
        makeRuntime = EditorGUILayout.Toggle("Make Runtime", makeRuntime);
        EditorGUI.FocusTextInControl("FeatureName");
        if (GUILayout.Button("Create new feature") || enterPressed)
        {
            Debug.Log("Creating new feature :" + featureName);
            if (!string.IsNullOrWhiteSpace(featureName))
            {
                Debug.Log("Feature name valid");
                featureName = featureName[..1].ToUpper() + featureName[1..];
                CreateNewProject(featureName, makeData, makeEditor, makeRuntime);
                CreateAssemblies(featureName, makeData, makeEditor, makeRuntime);
                PopupEditor popup = ScriptableObject.CreateInstance<PopupEditor>();
                popup.ShowPopup(10f, $"{featureName} feature created!");
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("Name field empty");
            }
        }
        else
        if (GUILayout.Button("Close"))
            Close();
    }

    private void CreateAssemblies(string featureName, bool makeData, bool makeEditor, bool makeRuntime)
    {
        Debug.Log("Creating Feature Assemblies");
        string systemFeatureStorePath = Path.Combine(Application.dataPath, featureStorePath);
        if (makeData)
            CreateAssembly(systemFeatureStorePath, featureName, "Data", new string[] { }, new string[] { });
        if (makeRuntime)
            CreateAssembly(systemFeatureStorePath, featureName, "Runtime", new string[] { }, new string[] { });
        if (makeEditor)
            CreateAssembly(systemFeatureStorePath, featureName, "Editor", new string[] { "Editor" }, new string[] { });

    }

    private void CreateAssembly(string systemFeatureStorePath, string featureName, string subfolder, string[] includePlatforms, string[] references)
    {

        string JsonPath = Path.Combine(systemFeatureStorePath, featureName, subfolder, $"{featureName}.{subfolder}.asmdef");
        AssemblyDefinition asmdef = new()
        {
            name = $"{featureName}.{subfolder}",
            rootNamespace = $"{featureName}.{subfolder}",
            references = references,
            includePlatforms = includePlatforms
        };
        File.WriteAllText(JsonPath, JsonUtility.ToJson(asmdef));
        Debug.Log("Assembly created at " + JsonPath);
    }

    private void CreateNewProject(string featureName, bool makeData, bool makeEditor, bool makeRuntime)
    {
        Debug.Log("Creating Feature Folders");
        string systemFeatureStorePath = Path.Combine(Application.dataPath, featureStorePath);
        Debug.Log("Creating " + Path.Combine(systemFeatureStorePath, featureName));
        Directory.CreateDirectory(Path.Combine(systemFeatureStorePath, featureName));
        if (makeData)
            Directory.CreateDirectory(Path.Combine(systemFeatureStorePath, featureName, "Data"));
        if (makeEditor)
            Directory.CreateDirectory(Path.Combine(systemFeatureStorePath, featureName, "Editor"));
        if (makeRuntime)
            Directory.CreateDirectory(Path.Combine(systemFeatureStorePath, featureName, "Runtime"));
    }


}