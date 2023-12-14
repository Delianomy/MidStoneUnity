using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    [Header("Enemy properties")]
    public float currentHealth = 250.0f;
    public float maxSpeed;
    private SpriteRenderer spriteRenderer;
    GAME gameManager;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GAME").GetComponent<GAME>();
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            gameManager.killCount += 1;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 
    }
}
