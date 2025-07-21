namespace UpdatePublisher.Runtime
{
    public abstract class ManagedBehaviour : DebugBehaviour.Runtime.VerboseMonoBehaviour, IUpdateObserver
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

