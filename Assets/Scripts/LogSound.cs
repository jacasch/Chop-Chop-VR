using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LogSound : MonoBehaviour {

    public AudioClip[] SplitSounds;
    public AudioClip[] CutSounds;
    private AudioSource source;

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlaySplitSound() {
        PlayRandomSound(SplitSounds);
    }

    public void PlayCutSound()
    {
        PlayRandomSound(CutSounds);
    }

    private void PlayRandomSound(AudioClip[] sounds){
        GetComponent<AudioSource>().clip = sounds[Random.Range(0, sounds.Length - 1)];
        GetComponent<AudioSource>().Play();
    }
}
