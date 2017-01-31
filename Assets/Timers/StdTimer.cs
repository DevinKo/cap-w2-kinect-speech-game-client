using System.Timers;
using UnityEngine;
using Assets.GameScripts;
using Assets.Toolbox;

namespace Assets.Timers
{
    class StdTimer : MonoBehaviour
    {
        public float TimeInterval = 30;
        private float _timer;
        private Toolbox.Toolbox toolbox;

        private void Start()
        {
            toolbox = FindObjectOfType<Toolbox.Toolbox>();
            ResetTimer(TimeInterval);
        }

        private void Update()
        {
            if (_timer <= Time.time)
            {
                
            }
        }

        private void ResetTimer(float timeInterval)
        {
            _timer = Time.time + timeInterval;
        }
    }
}
