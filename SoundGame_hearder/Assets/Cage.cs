using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    // Start is called before the first frame update
    public float minCageDistance;
    public float respawnDistance;
    public SoundManager soundManager;
    public AudioSource audioSource;
    public AudioSource audioSourceCaged;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRespawnDistance()
    {
        return respawnDistance;
    }

    public Vector3 GetLocation()
    {
        return gameObject.transform.position;
    }

    public float GetMinCageDistance()
    {
        return minCageDistance;
    }

    public void PlayCagedSound()
    {
        audioSourceCaged.clip = soundManager.getAudioClip(ClipType.CreatureCaged);
        audioSourceCaged.Play();
    }
}
