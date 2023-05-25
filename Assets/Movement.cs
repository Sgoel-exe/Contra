using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public PlayerMovements playerInput;
    private Vector2 moveDirection = Vector2.zero;

    private InputAction moveAction;
    private InputAction fireAction;

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
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * jumpForce);
    }

    void Fire (InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }
}
