using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour {
    // References
    public GameLogic gameLogic;

    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            gameLogic.AddScore(1);
    }
}
