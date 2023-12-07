using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield2 : MonoBehaviour
{
    public GameObject shieldObject;

    private bool activeShield;

    private float shieldDuration = 5.0f;
    private float shieldCooldown = 10.0f;

    private float shieldTimer;


    void Start()
    {
        activeShield = false;
        shieldObject.SetActive(false);
        shieldTimer = 0.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !activeShield && shieldTimer <= 0.0f){
            shieldObject.SetActive(true);
            activeShield = true;
            shieldTimer = shieldDuration;
            Debug.Log("shield active, (active for 5s)");
        }

        if(activeShield){

            shieldTimer -= Time.deltaTime;

            if(shieldTimer <= 0.0f){
                shieldObject.SetActive(false);
                activeShield = false;
                shieldTimer = shieldCooldown;
            }
        }
        else if (shieldTimer > 0.0f){
            Debug.Log("on cooldown (10s cooldown)");
        }
 
    }



public bool ActiveShield{
    get{
        return activeShield;
    }
    set {
        activeShield = value;
    }
}

}