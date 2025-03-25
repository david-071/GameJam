using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipPlayer : MonoBehaviour
{
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame && transform.rotation.y == 0)
        {
            spr.flipX = true;
            transform.Rotate(0, 180, 0);
        }

        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame && transform.rotation.y != 0)
        {
            spr.flipX = false;
            transform.Rotate(0, -180, 0);
        }
    }
}
