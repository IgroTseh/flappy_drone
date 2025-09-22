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
    public GameObject flapButton;
    public GameObject pauseButton;


    private void Start()
    {
        // Disabling GameOver screen
        gameOverScreen.SetActive(false);


        // Game Over trigger logic
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.OnDroneBrake.AddListener(GameOver);
        }

        // Pause
        GamePause();
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
        PlaySoundEffect("gamover");
        UpdateGameRecords(playerScore);
        gameOverScreen.SetActive(true);
        flapButton.SetActive(false);
        if (player != null)
        {
            player.OnDroneBrake.RemoveListener(GameOver);
        }

    }

    public void GamePause()
    {
        
        
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseButton.SetActive(false);
            flapButton.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            pauseButton.SetActive(true);
            flapButton.SetActive(false);
        }    
    }

    public void ToTheMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateGameRecords(int score)
    {
        RecordsManager records = FindObjectOfType<RecordsManager>();

        if (records != null)
        {
            records.UpdateRecords(score);
        }
        else
        {
            Debug.LogWarning("Records object not found in scene!");
        }
    }

    public void PlaySoundEffect(string soundName)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(soundName);
        }
        else
        {
            Debug.LogWarning("SoundManager не найден. Ќе удалось воспроизвести звук: " + soundName);
        }
    }
}
