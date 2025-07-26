using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    // Variables
    [SerializeField] private float flapPower = 6f;
    private bool droneIsAlive = true;

    // References
    private Rigidbody2D rigidBody2D;
    private GameInput gameInput;

    void Start()
    {
        // Getting references
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameInput = FindObjectOfType<GameInput>();

        // Becoming an OnFlap listener
        if (gameInput != null)
        {
            gameInput.OnFlap.AddListener(Jump);
            Debug.Log("Подписан!");
        }

    }

    private void Jump()
    {
        if (droneIsAlive)
        {
            rigidBody2D.velocity = Vector2.up * flapPower * Time.deltaTime;
            Debug.Log("Прыжок!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //logicManager.GameOver();
        droneIsAlive = false;
        if (gameInput != null)
        {
            gameInput.OnFlap.RemoveListener(Jump);
        }
    }
}
