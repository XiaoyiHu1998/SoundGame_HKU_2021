using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePickUp : MonoBehaviour
{
    public Camera mainCamera;
    public SoundManager soundManager;
    public bool carrying;
    private PickUpAble target;

    GameObject carriedObject;

    public GameObject PickUpSpot;

    void Start(){
        
    }
    void Update(){
        if(carrying){
            Carry(carriedObject);
            CheckDrop();
        }else{
            Pickup();
        }
        target = FindObjectOfType<PickupHitbox>().target;
    }

    void Carry(GameObject o){
        //o.GetComponent<Rigidbody>().isKinematic = true;
        o.GetComponent<CapsuleCollider>().enabled = false;
        o.transform.position = new Vector3(PickUpSpot.transform.position.x, o.transform.position.y, PickUpSpot.transform.position.z);
        o.transform.parent = PickUpSpot.transform;
    }

    void Pickup(){
        /*
        if(Input.GetMouseButtonDown(0)){
            int x = Screen.width / 2;
            int y = Screen.height/2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 4)){
                Debug.DrawRay(ray.origin, hit.point, Color.red);
                PickUpAble p = hit.collider.GetComponent<PickUpAble>();
                if(p != null){
                    carrying = true;
                    carriedObject = p.gameObject;
                    Creature pickedUpCreature = carriedObject.GetComponent<Creature>();
                    pickedUpCreature.Pickup();
                    pickedUpCreature.PlayClip(soundManager.getAudioClip(ClipType.CreaturePickedUp), 0.5f);
                }
            }
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            if (target != null)
            {
                carrying = true;
                carriedObject = target.gameObject;
                Creature pickedUpCreature = carriedObject.GetComponent<Creature>();
                pickedUpCreature.Pickup();
                pickedUpCreature.PlayClip(soundManager.getAudioClip(ClipType.CreaturePickedUp), 0.5f);
            }
        }
    }

    void CheckDrop(){
        if(Input.GetMouseButtonUp(0)){
            DropObject();
        }
    }

    void DropObject(){
        carriedObject.transform.parent = null;
        //carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject.GetComponent<CapsuleCollider>().enabled = true;
        carrying = false;
        playSound();
    }

    

    void playSound(){
        carriedObject.GetComponent<Creature>().Drop();
    }
}
