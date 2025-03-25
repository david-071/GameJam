using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator mAnimator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpingPower = 10f;
    public float defaultGravity = 2f;
    bool isFacingRight;
    bool hasJumped = false;
    float groundedDelay = 0.1f;
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
                    GameManager.manager.isJumpingGlobal = false;
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
        if (!GameManager.manager.reflectionPlaying)
        {
            BasicControls();
        }
        else
        {

        }

        //Actualización de animaciones
        mAnimator.SetBool("isChanging", GameManager.manager.isChangingGlobal);
        mAnimator.SetBool("isEndingChange", GameManager.manager.isEndingChangeGlobal);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (!GameManager.manager.reflectionPlaying)
        {
            //Calcular movimiento mediante contexto
            horizontalMovement = context.ReadValue<Vector2>().x;
            if (horizontalMovement >= 0.1f)
            {
                transform.localScale = new Vector3(1f, 1f);
                mAnimator.SetBool("isRunning", true);
                GameManager.manager.isRunningGlobal = true;
                isFacingRight = true;
            }
            else if (horizontalMovement <= -0.1)
            {
                transform.localScale = new Vector3(-1f, 1f);
                mAnimator.SetBool("isRunning", true);
                GameManager.manager.isRunningGlobal = true;
                isFacingRight = false;
            }
            else
            {
                mAnimator.SetBool("isRunning", false);
                GameManager.manager.isRunningGlobal = false;
            }
            GameManager.manager.isFacingRight = isFacingRight;
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
                    mAnimator.SetBool("isJumping", true);
                    GameManager.manager.isJumpingGlobal = true;
                    hasJumped = true;
                    groundedDelay = 0f;
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
                    mAnimator.SetBool("isChanging", true);
                    GameManager.manager.isChangingGlobal = true;
                    GameManager.manager.isEndingChangeGlobal = false;
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


    //GameOver
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GameOver"))
        {
            GameManager.manager.isGameOver = true;
        }
    }

    //Cheat
    public void Teleport(InputAction.CallbackContext context)
    {
        transform.position = new Vector2(transform.position.x + 30, transform.position.y + 3);
        
    }
}