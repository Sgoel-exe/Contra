using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float fireRate = 2f;
    public float range = 15f;
    public int bulletDamage = 20;

    public float bulletLife = 2f;

    private float nextFire = 0f;
    private GameObject player;

    private enemyScript thisScript;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        thisScript = this.GetComponent<enemyScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < range)
        {
            //Debug.Log(distance);
            nextFire += Time.deltaTime;
        }
        if (nextFire > fireRate)
        {
            if (!isFacingPlayer())
            {
                thisScript.TransformFlip();
                
            }
            thisScript.stopMovment();
            animator.SetBool("isShooting", true);
            nextFire = 0f;
            Shoot();

            animator.SetBool("isShooting", false);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<EnemyBullet>().setDamage(bulletDamage);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, bulletLife);
    }

    public bool isFacingPlayer()
    {
        if(thisScript.body.velocity.x > 0f && transform.position.x - player.transform.position.x > 0f)
        {
            return false;
        }
        else if(thisScript.body.velocity.x < 0f && transform.position.x - player.transform.position.x < 0f)
        {
            return false;
        }
        return true;
    }
}
