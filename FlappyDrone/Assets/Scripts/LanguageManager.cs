using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LanguageManager : MonoBehaviour {
    public enum Language { Russian, English }
    public static LanguageManager Instance;
    public Language currentLanguage = Language.Russian;

    private Dictionary<string, string> russianDict;
    private Dictionary<string, string> englishDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAllTextsOnScene();
    }

    void InitializeDictionaries()
    {
        var ruTexts = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("tPlay", "Играть"),
            new KeyValuePair<string, string>("tRecords", "Рекорды"),
            new KeyValuePair<string, string>("tSettings", "Настройки"),
            new KeyValuePair<string, string>("tHowToPlay", "Как играть"),
            new KeyValuePair<string, string>("tTapToPay", "ТЫКНИ, ЧТОБЫ НАЧАТЬ"),
            new KeyValuePair<string, string>("tDroneWasShotDown", "ДРОН СБИТ"),
            new KeyValuePair<string, string>("tBackToMenu", "В меню"),
            new KeyValuePair<string, string>("tMusicON", "Музыка: ВКЛ"),
            new KeyValuePair<string, string>("tMusicOFF", "Музыка: ВЫКЛ"),
            new KeyValuePair<string, string>("tSoundON", "Звуки: ВКЛ"),
            new KeyValuePair<string, string>("tSoundOFF", "Звуки: ВЫКЛ"),
            new KeyValuePair<string, string>("tBack", "Назад"),
            new KeyValuePair<string, string>("tPlayAgain", "Играть Снова"),
            new KeyValuePair<string, string>("tQuit", "Выход"),
        };
        russianDict = ruTexts.ToDictionary(pair => pair.Key, pair => pair.Value);

        var enTexts = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("tPlay", "Play"),
            new KeyValuePair<string, string>("tRecords", "Records"),
            new KeyValuePair<string, string>("tSettings", "Settings"),
            new KeyValuePair<string, string>("tHowToPlay", "How to Play"),
            new KeyValuePair<string, string>("tTapToPay", "TAP TO PLAY"),
            new KeyValuePair<string, string>("tDroneWasShotDown", "DRONE WAS SHOT DOWN"),
            new KeyValuePair<string, string>("tBackToMenu", "Back to Menu"),
            new KeyValuePair<string, string>("tMusicON", "Music: ON"),
            new KeyValuePair<string, string>("tMusicOFF", "Music: OFF"),
            new KeyValuePair<string, string>("tSoundON", "Sound: ON"),
            new KeyValuePair<string, string>("tSoundOFF", "Sound: OFF"),
            new KeyValuePair<string, string>("tBack", "Back"),
            new KeyValuePair<string, string>("tPlayAgain", "Play Again"),
            new KeyValuePair<string, string>("tQuit", "Quit"),
        };
        englishDict = enTexts.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void SwitchLanguage()
    {
        int currentLangIndex = (int)currentLanguage;
        int nextLangIndex = (currentLangIndex + 1) % System.Enum.GetValues(typeof(Language)).Length;
        currentLanguage = (Language)nextLangIndex;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveLanguage();
        }

        ForceUpdateTexts();
        Debug.Log($"Язык изменен на: {currentLanguage}");
    }

    public string GetText(string tag)
    {
        string result = "";
        bool found = false;

        switch (currentLanguage)
        {
            case Language.Russian:
                found = russianDict.TryGetValue(tag, out result);
                break;
            case Language.English:
                found = englishDict.TryGetValue(tag, out result);
                break;
        }

        if (!found)
        {
            Debug.LogWarning($"Язык: {currentLanguage}. Перевод для тега '{tag}' не найден!");
            return "ERROR";
        }

        return result;
    }

    public void UpdateAllTextsOnScene()
    {
        Text[] allTexts = Resources.FindObjectsOfTypeAll<Text>();

        foreach (Text textComponent in allTexts)
        {
            if (textComponent.gameObject.scene.buildIndex != -1)
            {
                string objectTag = textComponent.gameObject.tag;

                if (objectTag != "Untagged")
                {
                    string newText = GetText(objectTag);
                    if (newText != "ERROR")
                    {
                        textComponent.text = newText;
                    }
                }
            }
        }
    }

    public void ForceUpdateTexts()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1;
        UpdateAllTextsOnScene();
        Time.timeScale = originalTimeScale;
    }
}
