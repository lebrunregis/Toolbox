using UnityEditor;
using UnityEngine;

public class LevelBuilder : EditorWindow
{
    [MenuItem("Assets/Create/Level", priority = 11)]
    static void CreateStructure()
    {
        LevelBuilder window = ScriptableObject.CreateInstance<LevelBuilder>();
        window.ShowUtility();
    }
}

