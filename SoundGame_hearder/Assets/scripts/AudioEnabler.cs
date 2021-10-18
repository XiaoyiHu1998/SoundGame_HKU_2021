using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnabler : MonoBehaviour
{
    public GameObject Source;

    void Start()
    {
        Source.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Source.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Source.SetActive(false);
        }
    }
}




