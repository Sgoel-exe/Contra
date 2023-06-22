using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShootMechanics : MonoBehaviour
{
    public PlayerMovements playerInput;

    public Transform firePoint;
    public GameObject bulletPrefab;
    //public GameObject MuzzleFlash;

    public Animator animator;
    public float animationTime = 0.5f;

    private InputAction fireAction;

    [SerializeField] private int magSize = 6;
    [SerializeField] private int currentBullets = 6;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private int reservedAmmo = 24;
    public InputAction reloadKey;
    public Text ammotext;

    [SerializeField] private float fireRate = 0.2f;
    private float timer = 0f;
    private void Awake()
    {
        playerInput = new PlayerMovements();
    }

    private void OnEnable()
    {
        fireAction = playerInput.Player.Fire;
        fireAction.Enable();
        fireAction.performed += Fire;

        reloadKey.Enable();
        reloadKey.performed += Reload;

    }

    private void OnDisable()
    {
        fireAction.Disable();
        reloadKey.Disable();
    }

    void Fire(InputAction.CallbackContext callbackContext)
    {
        fire(false);
    }

    void fire(bool isHeld)
    {
        if (canShoot())
        {
            currentBullets--;
            animator.SetBool("isShooting", true);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //Instantiate(MuzzleFlash, firePoint.position, Quaternion.Euler(0,0,0));
            Invoke("StopShoot", animationTime);
            
        }
        else
        {
            StartCoroutine(reload());
        }
    }

    void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine (reload());
    }

    public IEnumerator reload()
    {
        //Invoke("StopShoot", 0.01f);
        fireAction.Disable();
        yield return new WaitForSeconds(reloadTime);
        fireAction.Enable();
        if(reservedAmmo > 0)
        {
            reservedAmmo -= (magSize - currentBullets);
            currentBullets = magSize;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool canShoot()
    {
        if(currentBullets < 1)
        {
            return false;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void addAmmo(int amount)
    {
        reservedAmmo += amount;
    }
    public void setMagSize(int magSize)
    {
        this.magSize = magSize;
    }

    public void setCurrentBullet(int num)
    {
        this.currentBullets = num;
    }

    public void setReloadtime(float time)
    {
        this.reloadTime = time;
    }

    public void setReservedAmmo(int num)
    {
        this.reservedAmmo = num;
    }
    private void LateUpdate()
    {
        ammotext.text = (currentBullets.ToString() + "/" + reservedAmmo.ToString());
    }

    private void FixedUpdate()
    {
        if (fireAction.ReadValue<float>() > 0.1f)
        {
            if(timer >= fireRate)
            {
                timer = 0;
                fire(true);
            }
            else
            {
                timer += Time.smoothDeltaTime;
            }
        }

    }

    private void StopShoot()
    {
        animator.SetBool("isShooting", false);
    }
}
