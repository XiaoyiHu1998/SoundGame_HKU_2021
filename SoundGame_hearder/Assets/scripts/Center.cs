using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxCageDistance;
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

    public float GetMaxCageDistance()
    {
        return maxCageDistance;
    }
}
