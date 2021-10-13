using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    CreatureSound cs;
    [SerializeField] AudioSource captured;
    [SerializeField] AudioSource pickup;
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
        //if(other.CompareTag("Creature"))
        //{
        //    pickup.Play();
        //    other.GetComponent<CreatureSound>().isCaptured = true;
        //}
        if(other.CompareTag("Creature"))
        {
            pickup.Play();
        }
        
    }

    
}
