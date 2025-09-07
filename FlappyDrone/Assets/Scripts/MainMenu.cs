using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayMusicSafe("MenuTheme");
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void GameQuit()
    {
        {
            Application.Quit();
        }
    }


    private SoundManager GetSoundManager()
    {
        // Пытаемся найти существующий SoundManager
        SoundManager soundManager = SoundManager.Instance;

        // Если экземпляр не найден, выводим предупреждение
        if (soundManager == null)
        {
            Debug.LogWarning("SoundManager не найден. Убедитесь, что он добавлен на сцену.");
        }

        return soundManager;
    }

    private void PlayMusicSafe(string clipName)
    {
        SoundManager soundManager = GetSoundManager();
        if (soundManager != null)
        {
            soundManager.PlayMusic(clipName);
        }
    }
}
