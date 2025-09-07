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
        // �������� ����� ������������ SoundManager
        SoundManager soundManager = SoundManager.Instance;

        // ���� ��������� �� ������, ������� ��������������
        if (soundManager == null)
        {
            Debug.LogWarning("SoundManager �� ������. ���������, ��� �� �������� �� �����.");
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
