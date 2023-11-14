using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movement_speeeeeeed;
    public Rigidbody2D rb;
    private Vector2 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Processing Inputs 
       ProcessInput();
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
    void Move() {
        rb.velocity = new Vector2(moveDir.x * movement_speeeeeeed, moveDir.y * movement_speeeeeeed);
    
    }
}
