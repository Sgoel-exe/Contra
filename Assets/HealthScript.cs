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
    private Color ogColor;

    private bool isShield = false;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setHealth(curhealth, maxhealth);
        thisSprite = GetComponent<SpriteRenderer>();
        ogColor = thisSprite.color;
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

    public void setShield(bool val)
    {
        this.isShield = val;
    }

    public void TakeDamage(int damage)
    {
        if (isShield)
        {
            return;
        }
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
            curhealth = maxhealth;
        }
        healthBar.setHealth(curhealth, maxhealth);
    }

    public IEnumerator Shield(float time)
    {
        Color color = new Color(0, 255, 255);
        //float flTime = time / Time.deltaTime;
        this.setShield(true);
        for (float temp = 0; temp < 4; temp ++)
        {
            thisSprite.color = color;
            yield return new WaitForSeconds(time/8f);
            thisSprite.color = ogColor;
            yield return new WaitForSeconds(time/8f);
        }
        this.setShield(false);
    }

    public IEnumerator DamageStun()
    {   
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
