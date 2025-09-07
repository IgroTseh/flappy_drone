using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LanguageManager : MonoBehaviour {
    public enum Language { Russian, English, Chinese }
    public static LanguageManager Instance;
    public Language currentLanguage = Language.Russian;

    private Dictionary<string, string> russianDict;
    private Dictionary<string, string> englishDict;
    private Dictionary<string, string> chineseDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            UpdateAllTextsOnScene(); // Обновляем тексты при первой инициализации
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeDictionaries()
    {
        // Русский язык
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
            new KeyValuePair<string, string>("tBack", "Назад"), // НОВЫЙ ЭЛЕМЕНТ
        };
        russianDict = ruTexts.ToDictionary(pair => pair.Key, pair => pair.Value);

        // Английский язык
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
            new KeyValuePair<string, string>("tBack", "Back"), // НОВЫЙ ЭЛЕМЕНТ
        };
        englishDict = enTexts.ToDictionary(pair => pair.Key, pair => pair.Value);

        // Китайский язык
        var zhTexts = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("tPlay", "玩"),
            new KeyValuePair<string, string>("tRecords", "记录"),
            new KeyValuePair<string, string>("tSettings", "设置"),
            new KeyValuePair<string, string>("tHowToPlay", "游戏规则"),
            new KeyValuePair<string, string>("tTapToPay", "点击开始"),
            new KeyValuePair<string, string>("tDroneWasShotDown", "无人机被击落"),
            new KeyValuePair<string, string>("tBackToMenu", "返回菜单"),
            new KeyValuePair<string, string>("tMusicON", "音乐: 开"),
            new KeyValuePair<string, string>("tMusicOFF", "音乐: 关"),
            new KeyValuePair<string, string>("tSoundON", "音效: 开"),
            new KeyValuePair<string, string>("tSoundOFF", "音效: 关"),
            new KeyValuePair<string, string>("tBack", "返回"), // НОВЫЙ ЭЛЕМЕНТ
        };
        chineseDict = zhTexts.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void ChangeLang()
    {
        int currentLangIndex = (int)currentLanguage;
        int nextLangIndex = (currentLangIndex + 1) % System.Enum.GetValues(typeof(Language)).Length;
        currentLanguage = (Language)nextLangIndex;
        UpdateAllTextsOnScene();
        Debug.Log($"Язык изменен на: {currentLanguage}");
    }

    public void SetLanguage(int langIndex)
    {
        currentLanguage = (Language)langIndex;
        UpdateAllTextsOnScene();
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
            case Language.Chinese:
                found = chineseDict.TryGetValue(tag, out result);
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
                    else
                    {
                        Debug.LogWarning($"Объект с именем '{textComponent.gameObject.name}' имеет тег '{objectTag}', но перевод для него не найден.", textComponent.gameObject);
                    }
                }
            }
        }
    }

    public void OnSceneLoaded()
    {
        UpdateAllTextsOnScene();
    }
}