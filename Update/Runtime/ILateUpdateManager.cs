namespace Update.Runtime
{
    internal interface ILateUpdateManager
    {
        public abstract void LateUpdate();
        public abstract void RegisterLateUpdateObserver(IUpdateObserver observer);
        public abstract void UnregisterLateUpdateObserver(IUpdateObserver observer);
    }
}
