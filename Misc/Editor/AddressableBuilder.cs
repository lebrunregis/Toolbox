using UnityEditor;
using UnityEngine;

public class AddressableBuilder : EditorWindow
{
    [MenuItem("Assets/Create/Addressable", priority = 11)]
    private static void CreateStructure()
    {
        FeatureFolderStructureBuilderEditor window = ScriptableObject.CreateInstance<FeatureFolderStructureBuilderEditor>();
        window.ShowUtility();
    }
}
