using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {
    // Variables
    public int playerScore = 0;

    // References
    public Text scoreText;
    public GameObject gameOverScreen;

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
    }
}
