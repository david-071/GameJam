using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpingPower = 10f;
    public float defaultGravity = 2f;
    bool isFacingRight;
    float horizontalMovement;
    float reflectionMaxDelay;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    bool isGrounded;

    private void Start()
    {
        rb.gravityScale = defaultGravity;
        reflectionMaxDelay = GameManager.manager.reflectionChangeDelay;
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

    private void Update()
    {
        if (!GameManager.manager.reflectionPlaying)
        {
            BasicControls();
        }
        else
        {

        }
    }

    public void Movement(InputAction.CallbackContext context)
    {

        //Calcular movimiento mediante contexto
        horizontalMovement = context.ReadValue<Vector2>().x;
        if (horizontalMovement >= 0.1f)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1f, 1f);
        }
        else if (horizontalMovement <= -0.1)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1f, 1f);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!GameManager.manager.reflectionPlaying)
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
        if (!GameManager.manager.reflectionPlaying)
        {
            if (GameManager.manager.reflectionChangeDelay == reflectionMaxDelay)
            {
                if (context.performed && isGrounded)
                {
                    GameManager.manager.reflectionPlaying = true;
                    GameManager.manager.reflectionChangeDelay = 0f;
                    Debug.Log("Reflection playing: " + GameManager.manager.reflectionPlaying);
                }
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