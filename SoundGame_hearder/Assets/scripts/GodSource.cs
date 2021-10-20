using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSource : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLine(AudioClip audioClip, bool overrideCurrentLine)
    {
        if (audioSource.isPlaying && !overrideCurrentLine)
            return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
