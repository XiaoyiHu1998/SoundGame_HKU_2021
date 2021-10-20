using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSource : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    private List<AudioClip> allowedClips;
    private bool onlyAllowedClips;
    void Start()
    {
        onlyAllowedClips = false;
        allowedClips = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLine(AudioClip audioClip, bool overrideCurrentLine)
    {
        if (audioSource.isPlaying && !overrideCurrentLine || onlyAllowedClips && !allowedClips.Contains(audioClip))
            return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void setAllowedList(List<AudioClip> allowedClipsList)
    {
        onlyAllowedClips = true;
        allowedClips = allowedClipsList;
    }

    public void allowAll()
    {
        onlyAllowedClips = false;
    }
}
