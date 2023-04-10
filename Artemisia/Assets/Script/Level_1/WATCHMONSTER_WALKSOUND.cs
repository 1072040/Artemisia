using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATCHMONSTER_WALKSOUND : MonoBehaviour
{
        public AudioClip Walk, Walk2;

    public AudioSource AudioSources;
     public void Play_audio()
    {
        AudioSources.PlayOneShot(Walk);
    }
    public void Play_audio2()
    {
        AudioSources.PlayOneShot(Walk2);
    }
}
