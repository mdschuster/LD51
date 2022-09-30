using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WFG.Utilities
{
    /// <summary>
    /// Plays a random clip from the clip array.
    /// Use TimedDeath to destroy the object when the clip is finished.
    /// </summary>
    [RequireComponent(typeof(TimedDeath))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioObject : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip[] clips;
        
        private void Start()
        {
            if (clips.Length != 0)
            {
                _audioSource.loop = false;
                int index = Random.Range(0, clips.Length);
                _audioSource.clip = clips[index];
                _audioSource.Play();
            }
        }
    }
}