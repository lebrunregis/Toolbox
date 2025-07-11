namespace Update.Runtime
{

    internal interface IUpdatePublisher
    {
        public abstract void Update();

        public abstract void RegisterUpdateObserver(IUpdateObserver observer);

        public abstract void UnregisterUpdateObserver(IUpdateObserver observer);
    }
}
