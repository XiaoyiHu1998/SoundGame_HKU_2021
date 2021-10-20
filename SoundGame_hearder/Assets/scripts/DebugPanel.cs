using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public GameObject canvas;
    public bool panel = true;
    void Update()
    {
        if(panel){
            if(Input.GetKeyDown(KeyCode.P)){
                canvas.SetActive(false);
                panel = false;
            }
        }else{
           if(Input.GetKeyDown(KeyCode.P)){
                canvas.SetActive(true);
                panel = true;
            } 
        }
    }
}
