using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1f;

    [SerializeField] private GameObject enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    [SerializeField] private int spawnLimit;

    [SerializeField] private int enemy;
    private BoxCollider2D spawnBounds;

    private void Start()
    {
        spawnBounds = GetComponent<BoxCollider2D>();
        StartCoroutine(Spawner());
    }
    
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnTime);
        while (canSpawn)
        {
            yield return wait;

            if (enemy < spawnLimit)
            {
                enemy++;
                Vector2 spawnPos = new Vector2(
                    Random.Range(transform.position.x, transform.position.x + spawnBounds.size.x),
                    Random.Range(transform.position.y, transform.position.y + spawnBounds.size.y)
                );
                Instantiate(enemyPrefabs, spawnPos, Quaternion.identity);
            }

        }

        
    }
}
