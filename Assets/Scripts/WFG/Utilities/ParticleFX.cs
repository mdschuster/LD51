using System;
using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Class to be added as a component to a particle effects object.
    /// Meant for one off systems, like explosions
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleFX : MonoBehaviour
    {
        private ParticleSystem _particleFX;
        private void Start()
        {
            _particleFX = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = _particleFX.main;
            main.stopAction = ParticleSystemStopAction.Destroy;
            if (_particleFX.isStopped)
            {
                _particleFX.Play();
            }
        }
    }
}