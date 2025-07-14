using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(id: "LevelSelectorButton")]
public class LevelSelector : EnumField
{
    public string dataPath = Application.dataPath;
    public string levelStorePath = "_/Levels";
    public System.Enum enumerator;
    public List<string> levels = new();

    public void InitializeElement()
    {
        var info = new DirectoryInfo(Path.Combine(dataPath, levelStorePath));
        DirectoryInfo[] directoryInfo = info.GetDirectories();

        foreach (DirectoryInfo directory in directoryInfo)
        {
            FileInfo[] fileInfos = directory.GetFiles();
            foreach (FileInfo file in fileInfos)
            {
                if (file.Name.EndsWith(".unity"))
                {
                    levels.Add(file.FullName);
                }
            }
        }
        enumerator = CreateEnumFromArrays(levels);
        Init(enumerator);
        RegisterValueChangedCallback();
    }

    public static System.Enum CreateEnumFromArrays(List<string> list)
    {

        System.AppDomain currentDomain = System.AppDomain.CurrentDomain;
        AssemblyName levelEnum = new("LevelEnum");
        AssemblyBuilder ab = currentDomain.DefineDynamicAssembly(levelEnum, AssemblyBuilderAccess.Run);
        ModuleBuilder mb = ab.DefineDynamicModule(levelEnum.Name);
        EnumBuilder enumerator = mb.DefineEnum("LevelEnum", TypeAttributes.Public, typeof(int));

        int i = 0;
        enumerator.DefineLiteral("None", i); //Here = enum{ None }

        foreach (string names in list)
        {
            i++;
            enumerator.DefineLiteral(names, i);
        }

        System.Type finished = enumerator.CreateType();

        return (System.Enum)System.Enum.ToObject(finished, 0);
    }

    public void RegisterValueChangedCallback()
    {

    }
}