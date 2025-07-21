using System.Collections.Generic;
using UnityEngine;

namespace UpdatePublisher.Runtime
{
    public class FixedUpdatePublisher : DebugBehaviour.Runtime.VerboseMonoBehaviour, IFixedUpdatePublisher
    {
        private readonly List<IUpdateObserver> _observers = new();
        public void FixedUpdate()
        {
            int observerSize = _observers.Count;
            float time = Time.fixedDeltaTime;
            for (int i = 0; i < observerSize; i++)
            {
                _observers[i].ObservedUpdate(time);
            }
        }

        public void RegisterFixedUpdateObserver(IUpdateObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterFixedUpdateObserver(IUpdateObserver observer)
        {
            _observers.Remove(observer);
        }
    }

}

