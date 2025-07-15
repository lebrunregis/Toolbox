using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.Runtime
{
    public class JsonSaveIO : ISaveSerializer
    {
        public void Deserialize(out SaveFile file, SaveSlot slot, string stateId)
        {
            string path = GetSavePath(slot, stateId);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Save state '{stateId}' not found in {slot}");

            string json = File.ReadAllText(path);
            file = JsonConvert.DeserializeObject<SaveFile>(json);
        }

        public void Serialize(string path, SaveFile file)
        {
            TextWriter textWriter = new StringWriter();
            JsonSerializer serializer = new()
            {
                Formatting = Formatting.Indented
            };
            serializer.Serialize(textWriter, file);
            string output = JsonConvert.SerializeObject(file);
            Debug.Log(output);
        }

        private static string GetSavePath(SaveSlot slot, string stateId)
        {
            return $"{Application.persistentDataPath}.{slot}.{stateId}";
        }
    }
}
