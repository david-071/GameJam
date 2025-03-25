using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivation : MonoBehaviour
{
    public GameObject platform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            platform.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
