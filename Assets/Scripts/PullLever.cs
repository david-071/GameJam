using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PullLever : MonoBehaviour
{

    float pulledLever;


    public void PullingLever(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            pulledLever = GameManager.manager.selectedLever;
            GameManager.manager.pulledLever = pulledLever;
        }
    }


}
