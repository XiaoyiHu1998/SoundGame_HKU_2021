using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public AudioSource audioSource;
    public SoundManager soundManager;

    public float speed = 12f;
    public float IdleLineMinTime;
    public float IdleLineMaxTime;


    private Stopwatch IdleTimer;
    private float IdleTime;
    private bool wasWalking = false;
    private System.Random random;

    private void Start()
    {
        IdleTimer = new Stopwatch();
        random = new System.Random();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0.0f || z != 0.0f && !wasWalking)
        {
            wasWalking = true;
            audioSource.clip = soundManager.getAudioClip(ClipType.PlayerWalk);
            audioSource.Play();
            IdleTimer.Stop();
        }
        else if(x == 0.0f && z == 0.0f && wasWalking)
        {
            wasWalking = false;
            audioSource.Stop();
            IdleTimer.Start();
        }

        if(IdleTimer.ElapsedMilliseconds >= IdleTime)
        {
            if (wasWalking)
            {
                IdleTimer.Reset();
            }
            else
            {
                IdleTimer.Restart();
            }
            updateIdleTime();
            soundManager.playGodLine(GodLine.PlayerIdle);
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void updateIdleTime()
    {
        IdleTime = (float)(IdleLineMinTime + random.NextDouble() * (IdleLineMaxTime - IdleLineMinTime));
    }
}
