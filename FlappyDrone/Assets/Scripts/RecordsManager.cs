using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecordsManager : MonoBehaviour {
    private static RecordsManager instance;

    private int[] topScores = new int[6]; // 6 рекордов

    private Text[] recordTexts = new Text[6]; // 6 текстовых элементов

    void Awake()
    {
        // Реализация синглтона
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeRecords();
            FindRecordTextsByTags();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Обновляем отображение рекордов каждый кадр
        UpdateRecordsDisplay();
    }

    private void InitializeRecords()
    {
        // Инициализация нулевыми значениями
        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = 0;
        }
    }

    private void FindRecordTextsByTags()
    {
        // Находим текстовые элементы по тегам Record0-Record5
        for (int i = 0; i < 6; i++)
        {
            string tagName = "Record" + i;
            GameObject textObject = GameObject.FindWithTag(tagName);

            if (textObject != null)
            {
                recordTexts[i] = textObject.GetComponent<Text>();
                if (recordTexts[i] != null)
                {
                    Debug.Log("Найден текстовый элемент с тегом: " + tagName);
                }
                else
                {
                    Debug.LogWarning("У объекта с тегом " + tagName + " нет компонента Text");
                }
            }
            else
            {
                Debug.LogWarning("Не найден объект с тегом: " + tagName);
            }
        }
    }

    public void UpdateRecords(int newScore)
    {
        // Проверяем, является ли новый результат рекордом
        if (newScore <= topScores[topScores.Length - 1])
            return;

        // Находим позицию для вставки
        for (int i = 0; i < topScores.Length; i++)
        {
            if (newScore > topScores[i])
            {
                // Сдвигаем меньшие результаты вниз
                for (int j = topScores.Length - 1; j > i; j--)
                {
                    topScores[j] = topScores[j - 1];
                }

                // Вставляем новый результат
                topScores[i] = newScore;
                break;
            }
        }
    }

    private void UpdateRecordsDisplay()
    {
        // Если текстовые элементы не найдены, пытаемся найти их
        if (recordTexts == null || recordTexts.Length == 0 || recordTexts[0] == null)
        {
            FindRecordTextsByTags();

            // Если после поиска все еще не найдены, выходим
            if (recordTexts == null || recordTexts.Length == 0 || recordTexts[0] == null)
            {
                return;
            }
        }

        // Отображаем все 6 рекордов
        int displayCount = Mathf.Min(recordTexts.Length, topScores.Length);

        // Отображаем рекорды
        for (int i = 0; i < displayCount; i++)
        {
            if (recordTexts[i] != null)
            {
                if (topScores[i] > 0)
                {
                    recordTexts[i].text = $"{i + 1}. {topScores[i]}";
                }
                else
                {
                    recordTexts[i].text = $"{i + 1}. ---";
                }
            }
        }

        // Скрываем неиспользуемые текстовые элементы (если их больше 6)
        for (int i = displayCount; i < recordTexts.Length; i++)
        {
            if (recordTexts[i] != null)
            {
                recordTexts[i].text = "";
            }
        }
    }

    // Для получения текущих рекордов
    public int[] GetTopScores()
    {
        return topScores.ToArray();
    }

    // Для установки рекордов (для SaveManager)
    public void SetTopScores(int[] scores)
    {
        if (scores.Length != topScores.Length)
        {
            Debug.LogError("Invalid scores array length");
            return;
        }

        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = scores[i];
        }
    }
}