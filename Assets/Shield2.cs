using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield2 : MonoBehaviour
{
    public GameObject shieldObject;

    private bool activeShield;
    private bool cooldown;

    private float shieldDuration = 5.0f;
    private float shieldCooldown = 10.0f;

    private float shieldTimer;
    private float shieldCooldownTimer;

    void Start()
    {
        activeShield = false;
        cooldown = false;
        shieldObject.SetActive(false);
        shieldTimer = 0.0f;
        shieldCooldownTimer = 0.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !cooldown){
            if (!activeShield)
            {
                shieldObject.SetActive(true);
                activeShield = true;
                shieldTimer = 0;
            }
            else
            {
                shieldObject.SetActive(false);
                activeShield = false;
                shieldTimer = 0;
                cooldown = true;
            }
        }

        if(activeShield){
            shieldTimer += Time.deltaTime;
            Debug.Log("shield active, (active for " + Mathf.Round((shieldDuration - shieldTimer)) + ")");

            if (shieldTimer >= shieldDuration){
                shieldObject.SetActive(false);
                activeShield = false;
                shieldTimer = 0.0f;
                cooldown = true;
            }
        }

        if (cooldown){
            shieldCooldownTimer += Time.deltaTime;
            Debug.Log("On cooldown, (active for " + Mathf.Round((shieldCooldown - shieldCooldownTimer)) + ")");

            if (shieldCooldownTimer >= shieldCooldown) {
                cooldown = false;
                shieldCooldownTimer = 0.0f;
            }
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