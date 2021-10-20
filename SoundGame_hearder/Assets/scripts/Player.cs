using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float creatureRunDistance;
    public SoundManager soundManager;
    public AudioSource audioSource;
    public Stopwatch godIdleTimer;

    public float godIdleTimeMin;
    public float godIdleTimeMax;

    private float godIdleTime;
    private System.Random random;
    void Start()
    {
        godIdleTimer = new Stopwatch();
        godIdleTimer.Start();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if(godIdleTimer.ElapsedMilliseconds / 1000 >= godIdleTime)
        {
            soundManager.playGodLine(GodLine.GodIdle);
            godIdleTimer.Restart();
            updateGodIdleTime();
        }
    }

    public Vector3 GetLocation()
    {
        return gameObject.transform.position;
    }

    public float GetCreatureRunDistance()
    {
        return creatureRunDistance;
    }

    void updateGodIdleTime()
    {
        godIdleTime = (float)(godIdleTimeMin + random.NextDouble() * (godIdleTimeMax - godIdleTimeMin));
    }
}
