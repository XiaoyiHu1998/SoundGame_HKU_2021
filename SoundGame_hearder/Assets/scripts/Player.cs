using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float creatureRunDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetLocation()
    {
        return gameObject.transform.position;
    }

    public float GetCreatureRunDistance()
    {
        return creatureRunDistance;
    }
}
