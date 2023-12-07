using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{



public float allowedDistance = 5.0f;
public float pushBackForce = 10.0f;
private void OnTriggerEnter2D(Collider2D other){
    if(other.tag == "Enemy"){
    float distanceToEnemy = Vector2.Distance(transform.position, other.transform.position);
    if(distanceToEnemy < allowedDistance){
        // push the enemy back
        Vector2 pushDirection = (other.transform.position - transform.position).normalized;
        other.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        Debug.Log("pushing back enemy");

    }

    }

}
}
