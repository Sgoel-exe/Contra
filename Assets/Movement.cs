using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 20f;
    public Rigidbody2D rb;
    public PlayerMovements playerInput;
    public Transform goundCheck;
    public LayerMask groundLayer;
    
    private Vector2 moveDirection = Vector2.zero;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;

    private bool facingRight = true;

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
        if (moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, 0f);
    }

    void Fire (InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (IsGrounded())
        {
            Debug.Log("Jump2");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

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
}
