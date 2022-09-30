using System;
using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Destroys this GameObject after a preset time.
    /// </summary>
    public class TimedDeath : MonoBehaviour
    {
        public float timeToDeath;
        private float _timer;

        private void Start()
        {
            _timer = timeToDeath;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}