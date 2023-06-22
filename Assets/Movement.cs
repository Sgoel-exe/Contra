using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //Misc
    public Rigidbody2D rb;
    public PlayerMovements playerInput;
    public Transform goundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;

    //Movement
    [SerializeField] private float speed = 7f;
    [SerializeField] private float normalSpeed = 7f;
    [SerializeField] private float crouchSpeed = 3.5f;
    private float time = 0;
    private float appliedSpeed;
    public AnimationCurve movemntCurve;
    public float jumpForce = 25f;


    public Animator animator;
    
    //PlayerInputs
    private Vector2 moveDirection = Vector2.zero;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool facingRight = true;

    //wall jump
    private bool isWallSliding = false;
    private float wallSlideSpeed = 0.75f;
    //private bool isWallJumping = false;
    private float wallJumpDirection = -1f;
    //private float wallJumpDuration = 1f;
    public Vector2 wallJumpForce = new Vector2(30f, 20f);

    //crouch
    public BoxCollider2D playerCollider;
    public SpriteRenderer playerRender;
    private Vector2 ColliderStandingSize;
    private Vector2 standingOffset;
    public Vector2 crouchingOffset;
    public Vector2 ColliderCrouchSize;
    private Sprite standingSprite;
    public Sprite crouchSprite;

    //Dash
    public float dashForce = 20f;
    public float dashTime = 0.6f;
    private bool canDash = true;
    private bool isDashin = false;
    private TrailRenderer tr;
    
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
        playerCollider = GetComponent<BoxCollider2D>();
        playerRender = GetComponent<SpriteRenderer>();
        standingSprite = playerRender.sprite;
        ColliderStandingSize = playerCollider.size;
        standingOffset = playerCollider.offset;
        tr = GetComponent<TrailRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDashin)
        {
            return;
        }
        moveDirection = moveAction.ReadValue<Vector2>();

        //Set animation
        if(Mathf.Abs(moveDirection.x) > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        //Wierd jump/wallslide animation contorl
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

        //flipper
        if (moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }

        wallSlide();

        //Crouch
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (IsGrounded())
            {
                playerCollider.size = ColliderCrouchSize;
                playerCollider.offset = crouchingOffset;
                animator.SetBool("isCrouching", true);
                speed = crouchSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerCollider.size = ColliderStandingSize;
            playerCollider.offset = standingOffset;
            animator.SetBool("isCrouching", false);
            speed = normalSpeed;
        }

        //dash
        if(!IsGrounded() && !isWalled())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDash)
                {
                    canDash = false;
                    StartCoroutine(Dash());
                }
            }
        }
        else
        {
            canDash = true;
        }

        //timeManagerForMovement();
    }

    void FixedUpdate()
    {
        if (isDashin)
        {
            return;
        }
        Move();
        //time = 0;
        //wallJump();
    }

    void Move()
    {
        time += Time.deltaTime;
        appliedSpeed = movemntCurve.Evaluate(time) * speed;
        rb.velocity = new Vector2(moveDirection.x * appliedSpeed, rb.velocity.y);
    }

    private void timeManagerForMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

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
    
    public IEnumerator Dash()
    {
        isDashin = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0.25f;
        rb.velocity = new Vector2(Mathf.Sign(transform.rotation.y) * dashForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        tr.emitting = false;
        isDashin = false;
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
        rb.velocity = new Vector2(wallJumpForce.x * wallJumpDirection, wallJumpForce.y);
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
