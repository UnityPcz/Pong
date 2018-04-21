using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
   
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;
    int playerLeft;
    int playerRight;

    // Use this for initialization
    void Start()
    {
        //direction = Vector2.one.normalized; //direction is (1,1) normalized
        //radius = transform.localScale.x / 2; //half the width

        rb2d = GetComponent<Rigidbody2D>();
        GoBall();
    }
    
    
    
    //**************************************************
    private Rigidbody2D rb2d;
    private Vector2 vel;

    void GoBall()
    {
        direction = Vector2.one.normalized; //direction is (1,1) normalized 
        radius = transform.localScale.x / 2; //half the width
       
    }
    void ResetBall()
    {
        vel = Vector2.zero;
        rb2d.velocity = vel;
        transform.position = Vector2.zero;
    }
    void RestartGame()
    {
        ResetBall();
        
    }

    //**************************************************


    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        //Bounce off top and bottom 
        if (transform.position.y < GameMenager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameMenager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        //Game over 
        if(transform.position.x < GameMenager.bottomLeft.x + radius && direction.x < 0)
        {
            playerRight++;
            Debug.Log(playerRight);
            NewService();

        }
        if (transform.position.x > GameMenager.topRight.x - radius && direction.x > 0)
        {
            playerLeft++;
            Debug.Log(playerLeft);
            NewService();
        }
    }

    void NewService()
    {
        RestartGame();
   
    }

    //flip direction
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }

}
