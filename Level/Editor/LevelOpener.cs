
using Level.Data;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Level.Editor
{
    public static class LevelOpener
    {
        [OnOpenAsset]
        public static bool OnDoubleClick(int instanceId, int line, int row)
        {
            Object target = EditorUtility.InstanceIDToObject(instanceId);
            switch (target)
            {
                case LevelData levelData:
                    if (Application.isEditor)
                    {
                        levelData.OpenEditorLevel();
                    }
                    else
                    {
                        levelData.LoadLevel();
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}

