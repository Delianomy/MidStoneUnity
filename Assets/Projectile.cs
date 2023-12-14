using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public enum group {Player = 0, Enemy = 1};
    [SerializeField] Rigidbody2D rb;
    string[] groups = { "Player", "Enemy" };

    [Header("Projectile properties")]
    public Vector2 direction;
    public float speed;
    public group currentGroup;
    public float duration = 3;
    public float power = 1;

    float durationTimer = 0;

    // Update is called once per frame
    void Update(){
        UpdateTimer();
        rb.velocity = direction * speed;

        //Update the rotation
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        angle *= 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == groups[(int)currentGroup]) {
            return;
        }

        if(other.gameObject.tag == groups[(int)group.Player])
        {

        }

        if (other.gameObject.tag == groups[(int)group.Enemy]){
            Enemy enemyProperties = other.gameObject.GetComponent<Enemy>();
            if(enemyProperties == null) { Destroy(gameObject); }
            enemyProperties.TakeDamage(power);
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }

        
    }

    void UpdateTimer(){
        durationTimer += Time.deltaTime;
        if(durationTimer >= duration) {
            Destroy(gameObject);
        }
    }
}
