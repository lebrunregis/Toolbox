using UnityEngine;

namespace Facts.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region Publics
        public static FactDictionary<bool> m_gameFactsBoolean;
        public static FactDictionary<string> m_gameFactsStrings;
        public static FactDictionary<int> m_gameFactsInt;
        public static FactDictionary<float> m_gameFactsFloat;
        public static FactDictionary<Transform> m_gameFactsTransform;
        #endregion

    }
}
