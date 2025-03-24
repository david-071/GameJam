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

    [Header("GroundCheck")]
    bool isGroudned;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    public bool isReflection = false;

    private void Awake()
    {
        //Invertir reflejo
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

        //Comprobar Grounded
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            isGroudned = true;
        }
        else
        {
            isGroudned = false;
        }
    }


    public void Movement(InputAction.CallbackContext context)
    {
        //Calcular movimiento mediante contexto
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGroudned == true)
        {
            if (context.performed)
            {
                //Full salto
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (context.canceled)
            {
                //Medio salto
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    //Groundcheck Hitbox
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
