using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.TimeZoneInfo;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ReflectionMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    //Player reference
    public GameObject objectPlayer;
    Transform playerTransform;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpingPower = -10f;
    public float defaultGravity = -2f;
    float horizontalMovement;
    float reflectionMaxDelay;
    float transitionTime = 0f;


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
        if (GameManager.manager.reflectionPlaying)
        {
            rb.gravityScale = defaultGravity;
            BasicControls();
        }
        else
        {
            rb.gravityScale = 0;
            playerTransform = objectPlayer.transform;
            Vector2 playerPosition = playerTransform.position;
            playerPosition.y = -playerTransform.position.y;

            if (transitionTime > 0f){
                transitionTime -= Time.deltaTime;
                rb.position = Vector2.Lerp(rb.position, playerPosition, Time.deltaTime * 10);
            } else
            {
                rb.position = playerPosition;
            }
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {

        //Calcular movimiento mediante contexto
        horizontalMovement = context.ReadValue<Vector2>().x;
        if (horizontalMovement >= 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f);
        }
        else if (horizontalMovement <= -0.1)
        {
            transform.localScale = new Vector3(-1f, 1f);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (GameManager.manager.reflectionPlaying)
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
        if (GameManager.manager.reflectionPlaying)
        {
            if (GameManager.manager.reflectionChangeDelay == reflectionMaxDelay)
            {
                if (context.performed)
                {
                    GameManager.manager.reflectionPlaying = false;
                    GameManager.manager.reflectionChangeDelay = 0f;
                    Debug.Log("Reflection playing: " + GameManager.manager.reflectionPlaying);
                    //Hacemos un tp hacia el jugador
                    float dist = Vector2.Distance(transform.position, playerTransform.position);
                    transitionTime = 1.0f;
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