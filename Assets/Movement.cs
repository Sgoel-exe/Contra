using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;
    public float jumpForce = 25f;
    public Rigidbody2D rb;
    public PlayerMovements playerInput;
    public Transform goundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public Animator animator;
    
    private Vector2 moveDirection = Vector2.zero;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;

    private bool facingRight = true;

    private bool isWallSliding = false;
    private float wallSlideSpeed = 0.75f;

    public bool isWallJumping = false;
    private float wallJumpDirection = -1f;
    private float wallJumpTime = 0.4f;
    private float wallJumpCounter;
    private float wallJumpDuration = 1f;
    public Vector2 wallJumpForce = new Vector2(30f, 20f);


    private void Awake()
    {
        playerInput = new PlayerMovements();
        //playerInput.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        //playerInput.Player.Move.canceled += ctx => moveDirection = Vector2.zero;
    }
    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
        
        fireAction = playerInput.Player.Fire;
        fireAction.Enable();
        fireAction.performed += Fire;

        jumpAction = playerInput.Player.Jump;
        jumpAction.Enable();
        jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        moveAction.Disable();  
    }
    // Start is called before the first frame update 
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
            animator.SetBool("isJumping", false);
        }
        else if(!isWalled()){
            animator.SetBool("isWallJumping", false);
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

    void Fire (InputAction.CallbackContext context)
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
        if (isWallSliding)
        {
            isWallJumping = false;
            if(facingRight)
            {
                wallJumpDirection = -1f;
            }
            else
            {
                wallJumpDirection = 1f;
            }
            //wallJumpDirection = -transform.rotation.y;
            wallJumpCounter = wallJumpTime;
            //CancelInvoke("stopWallJump");
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if(wallJumpCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpForce.x * wallJumpDirection, wallJumpForce.y);
            wallJumpCounter = 0f;

            Flip();

            //Invoke("stopWallJump", wallJumpDuration);
        }
    }

    public void stopWallJump()
    {
        isWallJumping = false;
    }
    public bool isFalling()
    {
        return rb.velocity.y < 0;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }  

    void Die()
    {
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
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
}
