using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField]
    public float speed;

    float heightPaddle;

    string input;
    public bool isRight;

    // Use this for initialization
    void Start()
    {
        heightPaddle = transform.localScale.y;
        speed = 5f;
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;
        if (isRightPaddle)
        {
            //Place paddle on the right of screen
            pos = new Vector2(GameMenager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x; //move a bit to the left (paddle was outside the sceen)

            input = "PaddleRight";
        }
        else
        {
            //Place on left
            pos = new Vector2(GameMenager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;  //move a bit to the right (paddle was outside the sceen)

            input = "PaddleLeft";
        }
        //Update this paddle's position
        transform.position = pos;

        transform.name = input;
    }


    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        //Restrict paddle movement
        //If paddle is to low and user is contiuing to move down, stop
        if (transform.position.y < GameMenager.bottomLeft.y + heightPaddle / 2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameMenager.topRight.y - heightPaddle / 2 && move > 0)
        {
            move = 0;
        }

        transform.Translate(move * Vector2.up);

    }
}
