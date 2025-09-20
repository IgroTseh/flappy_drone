using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    // Изменяем значения по умолчанию на 0.5
    public float musicVolume = 0.5f;
    public float soundVolume = 0.5f;
    public bool musicMuted;
    public bool soundsMuted;

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Создаем источник для музыки
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicMuted ? 0 : musicVolume; // Устанавливаем громкость

            // Загружаем сохраненные настройки
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.LoadSound();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Воспроизведение музыки
    public void PlayMusic(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/" + clipName);
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = musicMuted ? 0 : musicVolume;
        musicSource.Play();
    }

    // Воспроизведение звука
    public void PlaySound(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);
        if (clip == null) return;

        AudioSource soundSource = gameObject.AddComponent<AudioSource>();
        soundSource.clip = clip;
        soundSource.volume = soundsMuted ? 0 : soundVolume;
        soundSource.Play();

        Destroy(soundSource, clip.length);
    }

    // Управление громкостью
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicMuted ? 0 : musicVolume;

        // Сохраняем настройки
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;

        // Сохраняем настройки
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void ToggleMusicMute()
    {
        musicMuted = !musicMuted;
        musicSource.volume = musicMuted ? 0 : musicVolume;

        // Сохраняем настройки
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void ToggleSoundsMute()
    {
        soundsMuted = !soundsMuted;

        // Сохраняем настройки
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    // Остановка музыки
    public void StopMusic()
    {
        musicSource.Stop();
    }
}