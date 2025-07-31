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
    [SerializeField] private float enemySpawnXoffset = 5f;
    [SerializeField] private float spwnProbability = 0.3f;
    [SerializeField] private float timer = 2f;


    void Update()
    {
        // Spawning tubes
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnTube();
            SpawnEnemy();
            timer = 0;
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
        float enemySpawnX = transform.position.x + enemySpawnXoffset;
        Vector3 enemySpwnPos = new Vector3(enemySpawnX , enemySpawnY, 0f);
        float probability = Random.Range(0f, 1f);

        if (probability < spwnProbability)
        {
            Instantiate(enemy, enemySpwnPos, transform.rotation);
        }
    }
}
