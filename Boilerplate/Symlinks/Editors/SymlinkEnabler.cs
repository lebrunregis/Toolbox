using System;
using System.Collections.Generic;
using System.IO;
using Paps.UnityToolbarExtenderUIToolkit;
using Parabox;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "SymlinkEnablerButton")]
public class SymlinkEnabler : Button
{
    private static SymlinksPopup popup;

    public struct LevelState
    {
        public string sourceDirectory;
        public string targetDirectory;
        public string name;
        public bool enabled;
        public bool isSymlink;
    }

    public void InitializeElement()
    {
        text = "Symlinks";
        clicked += () => buttonClickedCallback();
    }
    public void buttonClickedCallback()
    {
        popup = ScriptableObject.CreateInstance<SymlinksPopup>();
        popup.ShowModal();
    }

    public class SymlinksPopup : EditorWindow
    {
        public const string LEVEL_DIRECTORY = "Assets\\_\\Levels";
        public const string symlinkDirectory = "..\\symlinks";
        public List<LevelState> levelStates = new();

        public void CreateGUI()
        {
            // The "makeItem" function is called when the
            // ListView needs more items to render.
            Func<VisualElement> makeItem = () =>
            {
                VisualElement container = new();
                TextElement itemName = new();
                Toggle toggle = new();

                container.name = "container";
                itemName.name = "name";
                toggle.name = "enable";
                toggle.RegisterCallback<ClickEvent>(e => OnToggleCallback(toggle));

                container.Add(itemName);
                container.Add(toggle);

                return container;
            };

            // As the user scrolls through the list, the ListView object
            // recycles elements created by the "makeItem" function,
            // and invoke the "bindItem" callback to associate
            // the element with the matching data item (specified as an index in the list).
            Action<VisualElement, int> bindItem = (element, index) =>
            {
                element.Q<TextElement>("name").text = levelStates[index].name;

                Toggle toggle = element.Q<Toggle>("enable");
                toggle.value = levelStates[index].enabled;
                toggle.userData = index;
            };


            // Provide the list view with an explicit height for every row
            // so it can calculate how many items to actually display
            const int itemHeight = 16;

            ListView listView = new(levelStates, itemHeight, makeItem, bindItem)
            {
            };

            listView.style.flexGrow = 1.0f;

            rootVisualElement.Add(listView);

            Button cancelButton = new();
            cancelButton.text = "Cancel";
            cancelButton.RegisterCallback<ClickEvent>(OnCancelCallback);
            rootVisualElement.Add(cancelButton);

            Button updateButton = new();
            updateButton.text = "Update";
            updateButton.RegisterCallback<ClickEvent>(OnUpdateCallback);
            rootVisualElement.Add(updateButton);
        }

        private void OnCancelCallback(ClickEvent evt)
        {
            Close();
        }

        private void OnUpdateCallback(ClickEvent evt)
        {
            foreach (LevelState level in levelStates)
            {
                if (level.enabled)
                {
                    if (Directory.Exists(level.targetDirectory + "\\" + level.name))
                    {
                        Debug.Log("Directory " + level.name + " already exists, nothing to do.");

                    }
                    else
                    {
                        Debug.Log("Directory " + level.name + " doesn't exist yet, creating symlink.");
                        Directory.CreateDirectory(level.sourceDirectory);
                        SymlinkTool.Symlink(SymlinkType.Junction, level.sourceDirectory, level.targetDirectory);

                    }
                }
                else
                {
                    if (Directory.Exists(level.targetDirectory + "\\" + level.name))
                    {
                        Debug.Log("Directory " + level.name + " already exists, deleting symlink.");
                        Directory.Delete(level.targetDirectory + "\\" + level.name);
                        AssetDatabase.DeleteAsset(level.targetDirectory + "\\" + level.name + ".meta");
                    }
                    else
                    {
                        Debug.Log("Directory " + level.name + " doesn't exist, nothing to do.");
                    }
                }
            }
            AssetDatabase.Refresh();
            Close();
        }

        public void OnEnable()
        {
            BuildLevelList();
        }

        public void OnToggleCallback(Toggle toggle)
        {
            int index = (int)toggle.userData;
            LevelState levelState = levelStates[index];
            levelState.enabled = toggle.value;
            levelStates[index] = levelState;
        }

        private void BuildLevelList()
        {
            string[] symlinksDirectories = Directory.GetDirectories(symlinkDirectory);

            foreach (string directory in symlinksDirectories)
            {
                levelStates.Add(new LevelState
                {
                    sourceDirectory = directory,
                    targetDirectory = LEVEL_DIRECTORY,
                    name = Path.GetFileName(directory),
                    enabled = Directory.Exists(LEVEL_DIRECTORY + Path.GetFileName(directory)),
                    isSymlink = IsSymbolic(directory)
                });
            }
        }

        private bool IsSymbolic(string path)
        {
            FileInfo pathInfo = new(path);
            return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
            //Can give false positives but not in our use case
        }

        private bool FileExists(string filePath)
        {
            return Directory.Exists(filePath);
        }
    }
}