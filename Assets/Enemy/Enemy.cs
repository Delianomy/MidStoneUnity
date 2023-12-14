using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    [Header("Enemy properties")]
    public float currentHealth = 250.0f;
    public float maxSpeed;
    private SpriteRenderer spriteRenderer;
    public float power = 20.0f;
    GAME gameManager;
    [SerializeField]Sprite[] sprites;

    Dictionary<int, EnemyProperties> enemyList = new Dictionary<int, EnemyProperties>() {
        { 0, new EnemyProperties(10, 3, 100, 0)},
        { 1, new EnemyProperties(35, 2.5f, 250, 1)},
        { 2, new EnemyProperties(5, 5, 50, 2)},
        { 3, new EnemyProperties(10, 3, 100, 3)}
    };

    private void Start() {
       
    }

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GAME").GetComponent<GAME>();
        RandomizeStats();
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
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
        int valueIndex = Random.Range(0, 4);
        power = enemyList[valueIndex].power;
        maxSpeed = enemyList[valueIndex].maxSpeed;
        currentHealth = enemyList[valueIndex].currentHealth;
        spriteRenderer.sprite = sprites[enemyList[valueIndex].imageIndex];
    }
}
