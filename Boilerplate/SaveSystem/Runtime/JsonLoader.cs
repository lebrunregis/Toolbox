using System;
using System.IO;
using Facts.Runtime;
using Newtonsoft.Json;
using UnityEngine;
namespace SaveSystem.Runtime
{
    public class JsonLoader : MonoBehaviour
    {
        public static void Load(GameFactsStorage factStore, SaveSlot slot, string stateId)
        {
            var path = GetSavePath(slot, stateId);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Save state '{stateId}' not found in {slot}");

            var json = File.ReadAllText(path);
            var saveFile = JsonConvert.DeserializeObject<SaveFile>(json);

            factStore.Clear();

        }

        private static string GetSavePath(SaveSlot slot, string stateId)
        {
            return $"{Application.persistentDataPath}";
        }
    }
}