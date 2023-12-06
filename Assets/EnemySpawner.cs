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

    private void Start()
    {
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
                Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
            }

        }

        
    }
}
