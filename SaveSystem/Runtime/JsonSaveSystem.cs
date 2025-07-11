using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonSaveSystem : ISaveSystem
{
    public void Save(MonoBehaviour gamestate)
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

    public void Load(object gamestate)
    {
        //    var deserializedPlayer = JsonSerialization.FromJson<Player>(json);
    }
}
