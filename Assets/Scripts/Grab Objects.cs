using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrabObjects : MonoBehaviour
{
    public Transform objectPosition;
    private GameObject heldObject;
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame) 
        {
            if (heldObject == null)
            {
                CheckPickup();
            }
            else
            {
                Drop();
            }
        }
    }

    void CheckPickup()
    {
        Collider2D[] objectInRange = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D obj in objectInRange)
        {
            if (obj.CompareTag("Object"))
            {
                Pickup(obj.gameObject);
                break;
            }
        }
    }

    void Pickup(GameObject obj)
    {
        heldObject = obj;
        obj.transform.SetParent(objectPosition);
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void Drop()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
        heldObject = null;
    }
}
