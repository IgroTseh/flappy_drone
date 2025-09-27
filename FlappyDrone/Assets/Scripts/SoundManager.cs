using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

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

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicMuted ? 0 : musicVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/" + clipName);
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = musicMuted ? 0 : musicVolume;
        musicSource.Play();
    }

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

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicMuted ? 0 : musicVolume;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void ToggleMusicMute()
    {
        musicMuted = !musicMuted;
        musicSource.volume = musicMuted ? 0 : musicVolume;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void ToggleSoundsMute()
    {
        soundsMuted = !soundsMuted;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveSound();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
