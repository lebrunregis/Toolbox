using UnityEngine;

public abstract class ManagedBehaviour : MonoBehaviour, IUpdateObserver
{
    private void OnEnable()
    {
        ScaledUpdatePublisher.RegisterObserver(this);
    }

    private void OnDisable()
    {
        ScaledUpdatePublisher.UnregisterObserver(this);
    }

    public abstract void ObservedUpdate();
}
