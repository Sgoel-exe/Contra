using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootMechanics : MonoBehaviour
{
    public PlayerMovements playerInput;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public float animationTime = 0.5f;
    private InputAction fireAction;

    private void Awake()
    {
        playerInput = new PlayerMovements();
    }

    private void OnEnable()
    {
        fireAction = playerInput.Player.Fire;
        fireAction.Enable();
        fireAction.performed += Fire;
    }

    void Fire(InputAction.CallbackContext context)
    {
        animator.SetBool("isShooting", true);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Invoke("StopShoot", animationTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopShoot()
    {
        animator.SetBool("isShooting", false);
    }
}
