using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAll();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Сохранение всех данных
    public void SaveAll()
    {
        SaveRecords();
        SaveSound();
        SaveLanguage();
        PlayerPrefs.Save(); // Важно для WebGL
    }

    // Загрузка всех данных
    public void LoadAll()
    {
        LoadRecords();
        LoadSound();
        LoadLanguage();
    }

    #region Records Manager
    public void SaveRecords()
    {
        // Ищем объект RecordsManager в сцене
        RecordsManager recordsManager = FindObjectOfType<RecordsManager>();
        if (recordsManager == null) return;

        int[] topScores = recordsManager.GetTopScores();
        for (int i = 0; i < topScores.Length; i++)
        {
            PlayerPrefs.SetInt($"Record_{i}", topScores[i]);
        }
    }

    public void LoadRecords()
    {
        RecordsManager recordsManager = FindObjectOfType<RecordsManager>();
        if (recordsManager == null) return;

        int[] topScores = new int[6];
        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = PlayerPrefs.GetInt($"Record_{i}", 0);
        }

        // Устанавливаем загруженные рекорды
        recordsManager.SetTopScores(topScores);
    }
    #endregion

    #region Sound Manager
    public void SaveSound()
    {
        if (SoundManager.Instance == null) return;

        PlayerPrefs.SetFloat("MusicVolume", SoundManager.Instance.musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", SoundManager.Instance.soundVolume);
        PlayerPrefs.SetInt("MusicMuted", SoundManager.Instance.musicMuted ? 1 : 0);
        PlayerPrefs.SetInt("SoundsMuted", SoundManager.Instance.soundsMuted ? 1 : 0);
    }

    public void LoadSound()
    {
        if (SoundManager.Instance == null) return;

        SoundManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SoundManager.Instance.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        SoundManager.Instance.musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        SoundManager.Instance.soundsMuted = PlayerPrefs.GetInt("SoundsMuted", 0) == 1;

        // Обновляем состояние аудио
        SoundManager.Instance.SetMusicVolume(SoundManager.Instance.musicVolume);
    }
    #endregion

    #region Language Manager
    public void SaveLanguage()
    {
        if (LanguageManager.Instance == null) return;
        PlayerPrefs.SetInt("CurrentLanguage", (int)LanguageManager.Instance.currentLanguage);
    }

    public void LoadLanguage()
    {
        if (LanguageManager.Instance == null) return;

        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            LanguageManager.Instance.currentLanguage = (LanguageManager.Language)PlayerPrefs.GetInt("CurrentLanguage");
            LanguageManager.Instance.ForceUpdateTexts();
        }
    }
    #endregion

    // Вызывается при выходе из игры
    private void OnApplicationQuit()
    {
        SaveAll();
    }

    // Для WebGL: сохраняем при изменении состояния страницы
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveAll();
    }
}