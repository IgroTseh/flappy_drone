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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            gameLogic.AddScore(1);
        }
    }
}
