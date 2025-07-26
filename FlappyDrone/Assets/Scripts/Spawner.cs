using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour {
    // References
    public GameObject spwnObj;

    // Variables
    [SerializeField] private float heightOfffset = 15f;
    [SerializeField] private float spawnRate = 0f;
    private float timer = 2f;


    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnObj();
            timer = 0;
        }
    }

    private void SpawnObj()
    {
        float lowestPoint = transform.position.y - heightOfffset;
        float highestPoint = transform.position.y + heightOfffset;

        Vector3 spwnPos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0f);
        Instantiate(spwnObj, spwnPos, transform.rotation);
    }
}
