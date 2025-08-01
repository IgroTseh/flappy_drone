using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    // Variables
    [SerializeField] private float flapPower = 6f;
    private bool droneIsAlive = true;

    // References
    private Rigidbody2D rigidBody2D;
    private GameInput gameInput;
    [SerializeField] GameObject grenade;

    // Events
    public UnityEvent OnDroneBrake;

    void Start()
    {
        // Getting references
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameInput = FindObjectOfType<GameInput>();

        // Becoming an OnFlap listener
        if (gameInput != null)
        {
            gameInput.OnFlap.AddListener(Jump);
        }

    }

    private void Jump()
    {
        if (droneIsAlive)
        {
            rigidBody2D.velocity = Vector2.up * flapPower;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        droneIsAlive = false;

        if (gameInput != null)
        {
            gameInput.OnFlap.RemoveListener(Jump);
        }

        OnDroneBrake?.Invoke();
    }

    public void ThrowGrenade()
    {
        if (droneIsAlive)
        {
            Vector3 grndSpwnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(grenade, grndSpwnPos, transform.rotation);
        }
     }
}
