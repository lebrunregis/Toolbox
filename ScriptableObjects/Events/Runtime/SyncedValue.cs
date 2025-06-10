using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SyncedValue<T> : ScriptableObject
{
    [SerializeField]
    private T sourceValue;
    public List<UnityEvent<T>> listeners;
    public bool enabled;

    public T SourceValue { get => sourceValue; set => SetSourceValue(value); }

    private void SetSourceValue(T value)
    {
        sourceValue = value;
        Raise(sourceValue);
    }

    public T Subscribe(UnityEvent<T> listener)
    {
        listeners.Add(listener);
        return SourceValue;
    }

    public void Unsubscribe(UnityEvent<T> listener)
    {
        listeners.Remove(listener);
    }

    private void Raise(T val)
    {
        if (enabled)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(val);
            }
        }
    }
    private void OnEnable()
    {
        enabled = true;
    }

    private void OnDisable()
    {
        enabled = false;
    }
}
