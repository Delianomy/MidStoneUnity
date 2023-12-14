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
    [SerializeField] Animator animator;
    [SerializeField] Vector2 playerDirection = Vector2.zero;

    //Camera follow the player
    Camera cam;

    // Start is called before the first frame update
    void Start(){
        current_speed = movement_speeeeeeed;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Processing Inputs 
        ProcessInput();
        if (Input.GetKeyDown(KeyCode.Space)){  
            isDashing = true;
        }

        //Dash logic
        if (isDashing){
            dash_timer += Time.deltaTime;
            current_speed = dash_speed;
            if (dash_timer >= dashtime){
                isDashing = false;
                current_speed = movement_speeeeeeed;
                dash_timer = 0.0f;
            }
        }

        //Make the camera follow the player only when it exits 
        if(cam != null) {
            cam.transform.position = gameObject.transform.position;
        }
    }

    private void LateUpdate(){
        PlayAnimation();
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

    //Animation
    void PlayAnimation(){
        //Forgive me lord for what I'm about to do

        //The player is moving
        if(moveDir.magnitude > 0){
            //Up
            if (playerDirection.x == 0 && playerDirection.y == 1) {
                animator.Play("player_walk_up");
            }

            //Down
            if (playerDirection.x == 0 && playerDirection.y == -1) {
                animator.Play("player_walk_down");
            }

            //Left
            if (playerDirection.x == -1 && playerDirection.y == 0) {
                animator.Play("player_walk_left");
            }

            //Right
            if (playerDirection.x == 1 && playerDirection.y == 0) {
                animator.Play("player_walk_right");
            }
        }

        //The player is not moving
        else{
            //Up
            if (playerDirection.x == 0 && playerDirection.y == 1) {
                animator.Play("player_idle_up");
            }

            //Down
            if (playerDirection.x == 0 && playerDirection.y == -1) {
                animator.Play("player_idle_down");
            }

            //Left
            if (playerDirection.x == -1 && playerDirection.y == 0) {
                animator.Play("player_idle_left");
            }

            //Right
            if (playerDirection.x == 1 && playerDirection.y == 0) {
                animator.Play("player_idle_right");
            }
        }
    }
}

