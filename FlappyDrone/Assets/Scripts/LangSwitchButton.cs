using UnityEngine;
using UnityEngine.UI;

public class LangSwitchButton : MonoBehaviour {
    private Button button;

    void Start()
    {
        // Получаем компонент Button
        button = GetComponent<Button>();

        if (button != null)
        {
            // Назначаем обработчик клика
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Компонент Button не найден на этом объекте!");
        }
    }

    void OnButtonClick()
    {
        // Находим LanguageManager через синглтон и вызываем переключение языка
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SwitchLanguage();
            Debug.Log($"Язык переключен на: {LanguageManager.Instance.currentLanguage}");
        }
        else
        {
            Debug.LogError("LanguageManager не найден! Убедитесь, что он есть на сцене.");
        }
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}