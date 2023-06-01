using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public float jumpForce = 25f;
    public Rigidbody2D rb;
    public PlayerMovements playerInput;
    public Transform goundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;

    public Animator animator;
    
    private Vector2 moveDirection = Vector2.zero;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool facingRight = true;

    private bool isWallSliding = false;
    private float wallSlideSpeed = 0.75f;

    public bool isWallJumping = false;
    private float wallJumpDirection = -1f;
    private float wallJumpDuration = 1f;
    public Vector2 wallJumpForce = new Vector2(30f, 20f);

    private void Awake()
    {
        playerInput = new PlayerMovements();
    }
    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        moveAction.Enable();

        jumpAction = playerInput.Player.Jump;
        jumpAction.Enable();
        jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        moveAction.Disable();  
    }
    
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        if(Mathf.Abs(moveDirection.x) > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        if(!IsGrounded())
        {
            //animator.SetBool("isJumping", false);
            if(rb.velocity.y < 0 && !isWalled())
            {
                animator.SetBool("isFalling", true);
                animator.SetBool("isJumping", false);
            }
            else if(rb.velocity.y > 0)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
        }
        else if(!isWalled()){
            animator.SetBool("isWallJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (IsGrounded() || isWalled())
        {
            animator.SetBool("isFalling", false);
        }

        if (moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }
        wallSlide();
    }

    void FixedUpdate()
    {
        Move();
        //wallJump();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", true);
            //rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (isWalled())
        {
            animator.SetBool("isWallJumping", true);
            wallJump();
        }

        //animator.SetBool("isJumping", false);
    }   

    private void wallSlide()
    {
          if (isWalled() && !IsGrounded() && rb.velocity.y < 0)
        {
            isWallSliding = true;
            animator.SetBool("isWallSliding", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", false);
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }   
    }

    private void wallJump()
    {
        if(facingRight)
            {
            wallJumpDirection = -1f;
        }
            else
        {
            wallJumpDirection = -1f;
        }
        isWallJumping = true;
        rb.velocity = new Vector2(wallJumpForce.x * wallJumpDirection, wallJumpForce.y);
        Invoke("stopWallJump", wallJumpDuration);
    }

    public void stopWallJump()
    {
        isWallJumping = false;
    }
    public bool isFalling()
    {
        return rb.velocity.y < 0;
    }
    

    void Flip() 
    {         
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(goundCheck.position, 0.2f, groundLayer);
    }   

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovingPlatforms"))
        {
            rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovingPlatforms"))
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
   
}
