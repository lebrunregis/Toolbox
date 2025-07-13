using UnityEngine;

namespace Misc.Data
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState")]
    public class GameState : ScriptableObject
    {
        public GuidReference currentLevel = null;

    }
}
