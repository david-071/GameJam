using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivation : MonoBehaviour
{
    public GameObject platform;
    Collider2D hitBoxCollider;
    void Start()
    {
        hitBoxCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        platform.GetComponent<Collider2D>().isTrigger = false;
    }
}
