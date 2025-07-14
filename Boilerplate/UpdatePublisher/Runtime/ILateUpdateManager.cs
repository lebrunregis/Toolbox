namespace UpdatePublisher.Runtime
{
    internal interface ILateUpdateManager
    {
        abstract void LateUpdate();
        abstract void RegisterLateUpdateObserver(IUpdateObserver observer);
        abstract void UnregisterLateUpdateObserver(IUpdateObserver observer);
    }
}
