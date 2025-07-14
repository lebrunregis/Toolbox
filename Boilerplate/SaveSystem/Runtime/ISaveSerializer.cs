using UnityEngine;

namespace SaveSystem.Runtime
{
    public interface ISaveSerializer
    {
    public void Serialize(string path, SaveFile file );
        public void Deserialize( string path, out SaveFile file );
    }
}
