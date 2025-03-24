using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Collider2D objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCollider.isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCollider.isTrigger = false;
    }
}
