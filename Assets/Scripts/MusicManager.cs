using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFG.Utilities;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{


    private void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }



    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
