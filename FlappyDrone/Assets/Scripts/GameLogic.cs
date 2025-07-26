using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameLogic : MonoBehaviour {
    // Variables
    public int playerScore = 0;

    // References
    public Text scoreText;
    public GameObject gameOverScreen;
    public Player player;


    private void Start()
    {
        // Disabling GameOver screen
        gameOverScreen.SetActive(false);


        // Game Over trigger logic
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.OnDroneBrake.AddListener(GameOver);
            Debug.Log("Подписан!");
        }
    }

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        if (player != null)
        {
            player.OnDroneBrake.RemoveListener(GameOver);
        }
    }
}
