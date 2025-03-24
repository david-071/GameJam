using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject objectPlayer;
    public GameObject objectReflection;
    Transform playerTransform;

    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpingPower = 10f;
    public float defaultGravity = 8f;
    float horizontalMovement;
    
    
    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    bool isGrounded;


    public bool isReflection = false;
    bool lockMovement = false;

    private void Awake()
    {
        //Invertir reflejo
        if (isReflection == true)
        {
            defaultGravity *= -1;
            rb.gravityScale *= -1;
            jumpingPower *= -1;
        }
    }

    void BasicControls()
    {
        //Actualizar movimiento
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        //Comprobar Grounded
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Update()
    {

        if (!isReflection)
        {

            BasicControls();
        }
        else //Comprueba si es el reflejo
        {
            if (!lockMovement)
            {
                //Mientras controles al real este sigue todos sus movimientos
                rb.gravityScale = 0;
                playerTransform = objectPlayer.transform;
                rb.position = new Vector2(playerTransform.position.x, playerTransform.position.y * -1);
            }
            else
            {
                rb.gravityScale = defaultGravity;
                BasicControls();
            }
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (!isReflection && !lockMovement)
        {
            //Calcular movimiento mediante contexto
            horizontalMovement = context.ReadValue<Vector2>().x;

        }
        else if (isReflection && lockMovement) //Comprueba cuando el reflejo puede moverse
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!isReflection && !lockMovement)
        {
            if (isGrounded == true)
            {
                if (context.performed)
                {
                    //Salto
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                }
            }
        }
        else if (isReflection && lockMovement)
        {
            if (isGrounded == true)
            {
                if (context.performed)
                {
                    //Salto
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                }
            }
        }

    }

    public void ChangeReflection(InputAction.CallbackContext context)
    {
        if (!lockMovement)
        {
            if (context.performed && !isReflection && isGrounded)
            {
                Debug.Log(gameObject.name + " changed to true");
                lockMovement = true;
                //objectReflection.lockMovement = true;
                horizontalMovement = 0;
                rb.velocity = new Vector2(0, rb.velocity.y); //Parar al personaje
            }
        }
        else
        {
            if (context.performed)
            {
                 Debug.Log(gameObject.name + " changed to false");
                lockMovement = false;
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
