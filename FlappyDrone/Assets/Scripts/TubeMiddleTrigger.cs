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
        PlaySoundEffect("plusScore");
            gameLogic.AddScore(1);
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
