using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallID;


    void Update()
    {
        if (GameManager.manager.pulledLever == wallID)
        {
            Debug.Log("Wall: " + wallID + " destroyed");
            Destroy(this.gameObject);
        }
    }
}
