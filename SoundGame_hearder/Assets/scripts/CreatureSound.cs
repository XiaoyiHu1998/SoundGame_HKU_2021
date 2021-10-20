using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CreatureSound : MonoBehaviour
{
    
    [SerializeField] AudioSource roaming;

    public bool isCaptured; 
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
     
        if(!isCaptured)
        {
            roaming.Play();
        }
        else
        {
            roaming.Stop();
        }
    }

}
