using UnityEngine;

namespace SaveSystem.Runtime
{
    public interface ISaveSystem
    {
        void Save(MonoBehaviour gamestate);
        void Load(object gamestate);
    }

}