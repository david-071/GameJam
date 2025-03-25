using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_mode : MonoBehaviour
{
    private CameraController cam;
    public Transform areaCenter;
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform upLimit;
    public Transform downLimit;

    void Awake()
    {
        cam = FindObjectOfType<CameraController>();

        if (cam == null)
        {
            Debug.LogError("CameraController no encontrado en la escena.");
        }
        if (leftLimit == null || rightLimit == null || areaCenter == null)
        {
            Debug.LogError("No se han asignado todos los objetos en Camera_mode.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cam != null)
            {
                float newLeftMax = leftLimit.position.x;
                float newRightMax = rightLimit.position.x;
                float newUpMax = upLimit.position.y;
                float newDownMax = downLimit.position.y;
                cam.SetNewArea(areaCenter.position, newLeftMax, newRightMax, newUpMax, newDownMax);
            }
        }
    }
}