using UnityEngine;

public interface ISaveSystem
{
    void Save(MonoBehaviour gamestate);
    void Load(object gamestate);
}
