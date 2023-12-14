using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float dash_timer = 0.0f;
    public float movement_speeeeeeed;
    public float dash_speed;
    public float current_speed;

    public Rigidbody2D rb;
    private Vector2 moveDir;
    bool isDashing = false;
    public float dashtime = 0.2f;

    //Animation stuff
    private Animator animator;
    [SerializeField] Vector2 playerDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start(){
        current_speed = movement_speeeeeeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Processing Inputs 
        ProcessInput();
        if (Input.GetKeyDown(KeyCode.Space)){  
            isDashing = true;
        }


        if (isDashing){
            dash_timer += Time.deltaTime;
            current_speed = dash_speed;
            if (dash_timer >= dashtime){
                isDashing = false;
                current_speed = movement_speeeeeeed;
                dash_timer = 0.0f;
            }
        }

    }

    private void LateUpdate(){
        
    }

    void ProcessInput(){
        //Physics calculations 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY); //come back to finish this

        if(moveDir.magnitude > 0){
            playerDirection = moveDir;
        }
    }
    void FixedUpdate(){
        //Physics calculations 
        Move();
    }

    void Move(){
        rb.velocity = new Vector2(moveDir.x * current_speed, moveDir.y * current_speed);
    }
}

