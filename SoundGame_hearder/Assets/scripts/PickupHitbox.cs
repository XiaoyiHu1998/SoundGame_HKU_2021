using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHitbox : MonoBehaviour
{
    public PickUpAble target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger" + other.gameObject.name);


        PickUpAble p = other.gameObject.GetComponent<PickUpAble>();
        if (p != null)
        {
            target = p;
            Debug.Log("added target");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
       
        PickUpAble p = other.gameObject.GetComponent<PickUpAble>();
        if (p != null && p == target)
        {
            target = null;
            Debug.Log("cleared target");
        }
    }
}
