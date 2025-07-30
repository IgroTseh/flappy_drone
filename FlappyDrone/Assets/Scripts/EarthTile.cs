using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTile : MonoBehaviour {
    // Variables
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float teleportPoint = -20f;
    [SerializeField] private float startPosX = 10f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < teleportPoint)
        {
            // Teleport to default position
            transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
        }
    }
}