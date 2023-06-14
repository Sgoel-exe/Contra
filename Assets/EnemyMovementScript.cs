using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    //public int maxhealth = 100;
    //public int curhealth = 100;

    //public GameObject deathEffect;
    public Rigidbody2D body;

    public float speed = 5f;
    public Transform PointA;
    public Transform PointB;
    public Transform currentPoint;
    public bool isFacingLeft = true;

    //public HealthBar healthBar;

    public bool canMove = true;
    public float stopTime = 1f;
    private float temptimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = PointB;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        //Set Speed
        if(canMove)
        {
            if (currentPoint == PointB)
            {
                body.velocity = new Vector2(speed, 0);
            }
            else
            {
                body.velocity = new Vector2(-speed, 0);
            }
        }
        else
        {
            body.velocity = Vector2.zero;
            temptimer += Time.deltaTime;

            if(temptimer > stopTime)
            {
                canMove = true;
                temptimer = 0f;
            }
        }

        //Change current point
        if(Vector2.Distance(currentPoint.position, transform.position) < 0.5f && currentPoint == PointB)
        {
            currentPoint = PointA;
        }
        else if (Vector2.Distance(currentPoint.position, transform.position) < 0.5f && currentPoint == PointA)
        {
            currentPoint = PointB;
        }

        //Flip the sprite
        if(body.velocity.x > 0 && isFacingLeft)
        {
            flip();
        }
        else if(body.velocity.x < 0 && !isFacingLeft)
        {
            flip();
        }

    }

    public void stopMovement()
    {
        canMove = !canMove;
        body.velocity = Vector2.zero;
    }
    public void flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
