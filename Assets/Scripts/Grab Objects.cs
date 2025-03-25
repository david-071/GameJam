using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabObject;
    private int layerIndex;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && grabObject == null)
            {
                grabObject = hitInfo.collider.gameObject;
                grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObject.transform.position = grabPoint.position;
                grabObject.transform.SetParent(transform);
            }

            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                grabObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabObject.transform.SetParent(null);
                grabObject = null;
            }
        }
    }
}
