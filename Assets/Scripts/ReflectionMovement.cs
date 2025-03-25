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
    private Animator mAnimator;

    //Player reference
    public GameObject objectPlayer;
    Transform playerTransform;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpingPower = -10f;
    public float defaultGravity = -2f;
    bool isFacingRight;
    bool hasJumped = false;
    float groundedDelay = 0.1f;
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
        mAnimator = GetComponent<Animator>();
    }

    void BasicControls()
    {
        //Actualizar movimiento
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        //Comprobar Grounded
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            if (hasJumped)
            {
                groundedDelay += Time.deltaTime;
                if (groundedDelay >= 0.1f && Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
                {
                    mAnimator.SetBool("isJumping", false);
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = true;
            }
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

            mAnimator.SetBool("isRunning", GameManager.manager.isRunningGlobal);
            mAnimator.SetBool("isJumping", GameManager.manager.isJumpingGlobal);

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
            transform.localScale = new Vector3(1f, transform.localScale.y);
            mAnimator.SetBool("isRunning", true);
        }
        else if (horizontalMovement <= -0.1)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y);
            mAnimator.SetBool("isRunning", true);
        }
        else
        {
            mAnimator.SetBool("isRunning", false);
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
                    mAnimator.SetBool("isJumping", true);
                    hasJumped = true;
                    groundedDelay = 0f;
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
                    isFacingRight = GameManager.manager.isFacingRight;
                    if (isFacingRight)
                    {
                        transform.localScale = new Vector3(1f, transform.localScale.y);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1f, transform.localScale.y);
                    }
                    GameManager.manager.reflectionPlaying = false;
                    GameManager.manager.reflectionChangeDelay = 0f;
                    Debug.Log("Reflection playing: " + GameManager.manager.reflectionPlaying);
                    //Hacemos un tp hacia el jugador
                    float dist = Vector2.Distance(transform.position, playerTransform.position);
                    transitionTime = 1.0f;
                    GameManager.manager.isChangingGlobal = false;
                    GameManager.manager.isEndingChangeGlobal = true;
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