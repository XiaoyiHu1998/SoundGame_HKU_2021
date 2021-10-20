using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public bool intro = true;
    public bool intro2 = false;
    public bool walking = false;
    public bool near = false;
    public bool pickup = false;
    public bool drop = false;
    public bool end = false;

    public AudioClip[] godTutorial;
    public AudioSource source;
    public GameObject soundScape;
    public GameObject player;
    public GameObject cam;
    public float timer;
    public GameObject creature;
    public GameObject cage;
    public AudioSource win;
    void Start()
    {
        source.clip = godTutorial[0];
        source.Play();
    }
    void Update(){
        if(intro){
            Intro();
        }
        if(intro2){
            Intro2();
        }
        if(walking){
            Walking();
        }
        if(near){
            Near();
        }
        if(pickup){
            Pickup();
        }
        if(drop){
            Drop();
        }
        if(end){
            End();
        }
    }

    void Intro(){
        if(!source.isPlaying){
            intro = false;
            intro2 = true;
            source.clip = godTutorial[1];
            source.Play();
            soundScape.SetActive(true);
        }
    }
    void Intro2(){
        if(!source.isPlaying){
            player.GetComponent<PlayerMovement>().enabled = true;
            cam.GetComponent<MouseLook>().enabled = true;
            timer += 1 * Time.deltaTime;
            if(timer >= 10){
                intro2 = false;
                walking = true;
                source.clip = godTutorial[2];
                source.Play();
            }
        }
    }
    void Walking(){
        if(!source.isPlaying){
            creature.SetActive(true);
            if(Vector3.Distance(player.transform.position, creature.transform.position) <= 4){
                walking = false;
                near = true;
                source.clip = godTutorial[3];
                source.Play();
            }
        }
    }
    void Near(){
        if(!source.isPlaying){
            cam.GetComponent<CreaturePickUp>().enabled = true;
            if(cam.GetComponent<CreaturePickUp>().carrying == true){
                near = false;
                pickup = true;
                source.clip = godTutorial[4];
                source.Play();
            }
        }
    }
    void Pickup(){
        if(!source.isPlaying){
            cage.SetActive(true);
            if(Vector3.Distance(cage.transform.position, creature.transform.position) <= 5 && cam.GetComponent<CreaturePickUp>().carrying == false){
                pickup = false;
                drop = true;
                creature.SetActive(false);
                cage.SetActive(false);
                win.Play();
                source.clip = godTutorial[5];
                source.Play();
                timer = 0;
            }
        }
    }
    void Drop(){
        if(!source.isPlaying){
            timer += 1*Time.deltaTime;
            if(timer >= 4){
                drop = false;
                end = true;
                player.GetComponent<PlayerMovement>().enabled = false;
                cam.GetComponent<MouseLook>().enabled = false;
                soundScape.SetActive(false);
                source.clip = godTutorial[6];
                source.Play();
            }
        }
    }
    void End(){
        if(!source.isPlaying){
            SceneManager.LoadScene(1);
        }
    }
}
