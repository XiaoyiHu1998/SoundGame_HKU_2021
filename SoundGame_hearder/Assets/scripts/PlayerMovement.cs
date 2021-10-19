using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public AudioSource audioSource;
    public SoundManager soundManager;

    public float speed = 12f;

    private bool wasWalking = false;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0.0f && z != 0.0f && !wasWalking)
        {
            wasWalking = true;
            audioSource.clip = soundManager.getAudioClip(ClipType.PlayerWalk);
            audioSource.Play();
        }
        else if(x == 0.0f && z == 0.0f && wasWalking)
        {
            wasWalking = false;
            audioSource.Stop();
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
