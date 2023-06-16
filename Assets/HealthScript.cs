using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxhealth = 100;
    public int curhealth = 100;
    public HealthBar healthBar;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        //curhealth = maxhealth;
        healthBar.setHealth(curhealth, maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        curhealth -= damage;
        Instantiate(blood, transform.position, Quaternion.identity);
        healthBar.setHealth(curhealth, maxhealth);
        if (curhealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
    }
}
