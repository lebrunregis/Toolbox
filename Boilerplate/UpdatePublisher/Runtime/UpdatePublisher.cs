using System.Collections.Generic;
using UnityEngine;

namespace UpdatePublisher.Runtime
{
    public class UpdatePublisher : DebugBehaviour.Runtime.VerboseMonoBehaviour, IUpdatePublisher
    {
        private readonly List<IUpdateObserver> _observers = new();
        public void Update()
        {
            int observerSize = _observers.Count;
            float time = Time.deltaTime;
            for (int i = 0; i < observerSize; i++)
            {
                _observers[i].ObservedUpdate(time);
            }
        }

        public void RegisterUpdateObserver(IUpdateObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterUpdateObserver(IUpdateObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}


