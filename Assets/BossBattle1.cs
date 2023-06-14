using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattle1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float fireRate = 0.5f;
    public float range = 15f;
    public int bulletDamage = 40;

    public float bulletLife = 2f;

    private float nextFire = 0f;
    private GameObject player;

    private EnemyHealthScript thishealth;
    // Start is called before the first frame update
    void Start()
    {
        thishealth = this.GetComponent<EnemyHealthScript>();
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
            //animator.SetBool("isShooting", true);
            nextFire = 0f;
            Shoot();

            //animator.SetBool("isShooting", false);
        }

        if(thishealth.curHealth <= 100)
        {
            SceneManager.LoadScene("Start");
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
}
