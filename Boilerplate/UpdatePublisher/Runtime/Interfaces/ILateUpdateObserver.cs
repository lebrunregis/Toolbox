namespace UpdatePublisher.Runtime
{
    public interface ILateUpdateObserver
    {
        void ObservedLateUpdate(float deltatime);
    }

}

