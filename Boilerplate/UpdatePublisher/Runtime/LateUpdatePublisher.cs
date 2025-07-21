using System.Collections.Generic;
using UnityEngine;

namespace UpdatePublisher.Runtime
{
    public class LateUpdatePublisher : DebugBehaviour.Runtime.VerboseMonoBehaviour, ILateUpdateManager
    {
        private readonly List<IUpdateObserver> _observers = new();
        private void LateUpdate()
        {
            int observerSize = _observers.Count;
            float time = Time.deltaTime;
            for (int i = 0; i < observerSize; i++)
            {
                _observers[i].ObservedUpdate(time);
            }
        }

        void ILateUpdateManager.LateUpdate()
        {
            LateUpdate();
        }

        public void RegisterLateUpdateObserver(IUpdateObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterLateUpdateObserver(IUpdateObserver observer)
        {
            _observers.Remove(observer);
        }
    }

}

