using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    public float jumpingPower = 10f;


    public bool isReflection = false;

    private void Awake()
    {
        if (isReflection == true)
        {
            rb.gravityScale *= -1;
            jumpingPower *= -1;
        }
    }

    void Update()
    {
        //Actualizar movimiento
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }


    public void Movement(InputAction.CallbackContext context)
    {
        //Calcular movimiento mediante contexto
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //Full salto
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if(context.canceled)
        {
            //Medio salto
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
}
