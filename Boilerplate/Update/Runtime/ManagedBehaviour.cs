using UnityEngine;

namespace Update.Runtime
{
    public abstract class ManagedBehaviour : MonoBehaviour, IUpdateObserver
    {
        private void OnEnable()
        {
            UpdatePublishers.updatePublisher.RegisterUpdateObserver(this);
        }

        private void OnDisable()
        {
            UpdatePublishers.updatePublisher.UnregisterUpdateObserver(this);
        }

        public abstract void ObservedUpdate(float deltatime);
    }

}

