using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // References
    public GameObject spwnObj;
    public GameObject enemy;

    // Variables
    [SerializeField] private float heightOfffset = 15f;
    [SerializeField] private float spawnRate = 0f;
    [SerializeField] private float enemySpawnY = -5f;
    private float timer = 2f;
    private float enemyTimer = 2f;
    private bool tubeIsSpawned;

    void Update()
    {
        // Spawning tubes
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
            tubeIsSpawned = false;
        }
        else
        {
            SpawnTube();
            timer = 0;
            tubeIsSpawned = true;
        }

        // Spawning enemys
        if (enemyTimer < spawnRate * 2.5f)
        {
            enemyTimer += Time.deltaTime;
        }
        else
        {
            if (!tubeIsSpawned)
            {
                SpawnEnemy();
            }
            enemyTimer = 0;
        }
    }

    private void SpawnTube()
    {
        float lowestPoint = transform.position.y - heightOfffset;
        float highestPoint = transform.position.y + heightOfffset;

        Vector3 spwnPos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0f);
        Instantiate(spwnObj, spwnPos, transform.rotation);
    }

    private void SpawnEnemy()
    {
        Vector3 enemySpwnPos = new Vector3(transform.position.x, enemySpawnY, 0f);
        Instantiate(enemy, enemySpwnPos, transform.rotation);
    }
}
