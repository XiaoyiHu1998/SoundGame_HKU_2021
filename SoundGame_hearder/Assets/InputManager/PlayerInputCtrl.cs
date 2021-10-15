using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCtrl : MonoBehaviour
{
    private DevinePowers devinePowers;
    private InputAction clap;

    private void Awake()
    {
        devinePowers = new DevinePowers();
    }

    private void OnEnable()
    {
        //clap = devinePowers.Default.Clap;
        //clap.Enable()

        devinePowers.Default.Clap.performed += isClapping;
        devinePowers.Default.Clap.Enable();

        devinePowers.Default.Rub.performed += isRubbing;
        devinePowers.Default.Rub.Enable();
    }

    private void isClapping(InputAction.CallbackContext obj)
    {
        Debug.Log("Well done, You are now clapping!");
    }
    private void isRubbing(InputAction.CallbackContext obj)
    {
        Debug.Log("Oh look at it, its enjoying your love");
    }

    private void OnDisable()
    {
        //clap.Disable()
        devinePowers.Default.Clap.Disable();
        devinePowers.Default.Rub.Disable();
    }
}
