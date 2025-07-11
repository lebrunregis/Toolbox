using UnityEngine;

namespace OOP.Runtime
{
    public class DebugBehaviour : MonoBehaviour
    {
        #region Debug

        public bool m_isVerbose;

        protected void Info(string msg)
        {
            if (!m_isVerbose)
            {
                Debug.Log(msg, this);
            }
        }

        #endregion
    }
}

