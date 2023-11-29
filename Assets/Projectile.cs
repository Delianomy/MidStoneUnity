using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public enum group {Player = 0, Enemy = 1};
    [SerializeField] Rigidbody2D rb;
    string[] groups = { "Player", "Enemy" };

    public Vector2 direction;
    public float speed;
    public float distance;
    public group currentGroup;

    // Update is called once per frame
    void Update(){
        rb.velocity = direction * speed;
    }

}
