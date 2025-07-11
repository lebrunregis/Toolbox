namespace Update.Runtime
{
    internal interface IFixedUpdatePublisher
    {
        public abstract void FixedUpdate();
        public abstract void RegisterFixedUpdateObserver(IUpdateObserver observer);
        public abstract void UnregisterFixedUpdateObserver(IUpdateObserver observer);
    }
}

