using UnityEngine;

namespace Rigidbody.Runtime
{
    public class Grabable : MonoBehaviour
    {
        public bool grabbed = false;

        private void OnEnable()
        {
            grabbed = false;
        }

        private void OnDisable()
        {
            grabbed = false;
        }
    }
}
