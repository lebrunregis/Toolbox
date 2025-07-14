using System.IO;
using Newtonsoft.Json;
using UnityEditor.Graphs;
using UnityEngine;

namespace SaveSystem.Runtime
{
    public class JsonSaveIO : ISaveSerializer
    {
        public void Deserialize( out SaveFile file, SaveSlot slot, string stateId)
        {
            var path = GetSavePath(slot, stateId);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Save state '{stateId}' not found in {slot}");

            var json = File.ReadAllText(path);
            var saveFile = JsonConvert.DeserializeObject<SaveFile>(json);

            factStore.Clear();
        }

        public void Serialize(string path, SaveFile file)
        {
            TextWriter textWriter = new StringWriter();
            JsonSerializer serializer = new()
            {
                Formatting = Formatting.Indented
            };
            serializer.Serialize(textWriter, gamestate);
            string output = JsonConvert.SerializeObject(gamestate);
            Debug.Log(output);
        }

        private static string GetSavePath(SaveSlot slot, string stateId)
        {
            return $"{Application.persistentDataPath}.{slot}.{stateId}";
        }
    }
}
