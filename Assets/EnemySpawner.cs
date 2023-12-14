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
    [SerializeField] GameObject[] possibleSpawnPos;

    public bool roomCompleted = false;
    List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] GAME GameManager;

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
                Vector2 spawnPos = possibleSpawnPos[Random.Range(0, possibleSpawnPos.Length)].transform.position;

                spawnedEnemies.Add(Instantiate(enemyPrefabs, spawnPos, Quaternion.identity));
            }

        }

        
    }

    private void FixedUpdate() {
        int deadEnemies = 0;
        for(int i = 0; i < spawnedEnemies.Count; i++) {
            if (spawnedEnemies[i] == null) {
                deadEnemies += 1;
            }
        }

        if(deadEnemies >= spawnLimit) { 
            roomCompleted = true;
            GameManager.OpenExitPortal();
        }

    }
}
