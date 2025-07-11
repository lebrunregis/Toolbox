using System.Collections.Generic;
using UnityEngine;

public class LateUpdateManager : MonoBehaviour
{
    private static readonly List<IUpdateObserver> _observers = new();
    private void Update()
    {
        int observerSize = _observers.Count;
        for (int i = 0; i < observerSize; i++)
        {
            _observers[i].ObservedUpdate();
        }
    }

    public static void RegisterObserver(IUpdateObserver observer)
    {
        _observers.Add(observer);
    }

    public static void UnregisterObserver(IUpdateObserver observer)
    {
        _observers.Remove(observer);
    }
}
