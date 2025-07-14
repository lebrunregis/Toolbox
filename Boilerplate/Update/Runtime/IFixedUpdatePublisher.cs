namespace Update.Runtime
{
    internal interface IFixedUpdatePublisher
    {
        abstract void FixedUpdate();
        abstract void RegisterFixedUpdateObserver(IUpdateObserver observer);
        abstract void UnregisterFixedUpdateObserver(IUpdateObserver observer);
    }
}

