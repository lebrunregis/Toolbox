namespace UpdatePublisher.Runtime
{

    public interface IUpdateObserver
    {
        void ObservedUpdate(float deltaTime);
    }

}
