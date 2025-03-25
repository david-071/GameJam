using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollow : MonoBehaviour
{
    public GameObject target;
    Transform targetTransform;

    void Update()
    {
        targetTransform = target.transform;
        transform.position = new Vector2(targetTransform.position.x, transform.position.y);
    }
}
