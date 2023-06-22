using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxhealth = 100;
    public int curhealth = 100;
    public HealthBar healthBar;
    
    public GameObject blood;

    private SpriteRenderer thisSprite;
    //private Rigidbody2D rb;
    [SerializeField] private int numOStun = 10;
    [SerializeField] private float vanishTime = 0.1f;
    [SerializeField] private Color flashColor;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.setHealth(curhealth, maxhealth);
        thisSprite = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
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
        StartCoroutine(DamageStun());
        if (curhealth <= 0)
        {
            Die();
        }
    }

    public void addHealt(int amount)
    {
        curhealth += amount;
        if(curhealth > maxhealth)
        {
            maxhealth = curhealth;
        }
        healthBar.setHealth(curhealth, maxhealth);
    }

    public IEnumerator DamageStun()
    {
        Color ogColor = thisSprite.color;
        
        for(int temp = 0; temp < numOStun; temp ++)
        {
            thisSprite.color = flashColor;
            yield return new WaitForSeconds(vanishTime);
            thisSprite.color = ogColor;
            yield return new WaitForSeconds(vanishTime);
        }
        
    }
    void Die()
    {
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
    }
}
