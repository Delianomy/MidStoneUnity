using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float timer =0.0f;
    public float movement_speeeeeeed;
    public float dash_speed;
    public float current_speed;
    public Rigidbody2D rb;
    private Vector2 moveDir;
    bool isDashing = false;
    public float dashtime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        current_speed = movement_speeeeeeed;

    }

    // Update is called once per frame
    void Update()
    {
        //Processing Inputs 
        ProcessInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space button pressed!");
            isDashing = true;
        }
        if (isDashing)
        {
          
            timer += Time.deltaTime;
            current_speed = dash_speed;
            if (timer >= dashtime)
            {
                isDashing = false;
                current_speed = movement_speeeeeeed;
                timer = 0.0f;
            }
        }

    }


    void ProcessInput()
    {
        //Physics calculations 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY); //come back to finish this

    }
    void FixedUpdate()
    {
        //Physics calculations 
        Move();
    }
    void Move()
    {

        rb.velocity = new Vector2(moveDir.x * current_speed, moveDir.y * current_speed);
    }
}

