namespace Update.Runtime
{

    internal interface IUpdatePublisher
    {
        abstract void Update();

        abstract void RegisterUpdateObserver(IUpdateObserver observer);

        abstract void UnregisterUpdateObserver(IUpdateObserver observer);
    }
}
