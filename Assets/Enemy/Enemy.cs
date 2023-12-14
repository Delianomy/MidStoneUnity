using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Struct to help with randomizing stats
public struct EnemyProperties {
    
    public float power;
    public float maxSpeed;
    public float currentHealth;
    public int imageIndex;

    public EnemyProperties(float power_, float maxSpeed_, float currentHealth_, int imageIndex_) {
        power = power_;
        maxSpeed = maxSpeed_;
        currentHealth = currentHealth_;
        imageIndex = imageIndex_;   
    }
};

public class Enemy : MonoBehaviour{
    GAME gameManager;

    [Header("Enemy properties")]
    public float currentHealth = 250.0f;
    public float maxSpeed;
    public float power = 20.0f;

    //Sprites to swap between
    [SerializeField]Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    //Audio stuff
    AudioManager audioManager;

    //Dictionary containing the enemy's values 
    Dictionary<int, EnemyProperties> enemyList = new Dictionary<int, EnemyProperties>() {
        { 0, new EnemyProperties(10, 3, 100, 0)},
        { 1, new EnemyProperties(35, 2.5f, 250, 1)},
        { 2, new EnemyProperties(5, 5, 50, 2)},
        { 3, new EnemyProperties(10, 3, 100, 3)}
    };


    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GAME").GetComponent<GAME>();
        
        //Randomize the stats when they spawn
        RandomizeStats();
    }

    private void Update()
    {
        //When enemy dies
        if(currentHealth <= 0)
        {
            audioManager.PlaySFX(audioManager.death);
            Destroy(gameObject);
            gameManager.killCount += 1;
            gameManager.score += 50;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 
    }

    void RandomizeStats() {
        //Pick a random number to select which stats to use
        int valueIndex = Random.Range(0, 4);

        //Assign the stats
        power = enemyList[valueIndex].power;
        maxSpeed = enemyList[valueIndex].maxSpeed;
        currentHealth = enemyList[valueIndex].currentHealth;
        spriteRenderer.sprite = sprites[enemyList[valueIndex].imageIndex];
    }
}
