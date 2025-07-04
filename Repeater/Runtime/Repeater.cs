using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    public class Repeater : MonoBehaviour
    {
        public UnityEvent m_OnStartupEnd = new();
        public UnityEvent m_OnRepeat = new();

        public float startupTime = 3;
        public float repeatTime = 1;

        private float repeatDelta = 0;
        private float startupDelta = 1;
        [Tooltip("Infinite if negative, disables itself if zero.")]
        public int repeatCount = 0;

        private void OnEnable()
        {
            repeatDelta = repeatTime;
            startupDelta = startupTime;
        }

        // Update is called once per frame
        private void Update()
        {
            if (startupDelta < 0)
            {
                if (repeatDelta < 0)
                {
                    repeatDelta = repeatTime;
                    m_OnRepeat.Invoke();

                    if (repeatCount < 0)
                    {
                   //     Debug.Log("Repeating forever");
                    }
                    else if (repeatCount == 0)
                    {
                        this.enabled = false;
                    }
                    else
                    {
                        repeatCount--;
                    }
                }
                else
                {
                    repeatDelta -= Time.deltaTime;

                }
            }
            else
            {
                startupDelta -= Time.deltaTime;
                if (startupDelta < 0)
                {
                    m_OnStartupEnd.Invoke();
                }
            }
        }
    }
}
