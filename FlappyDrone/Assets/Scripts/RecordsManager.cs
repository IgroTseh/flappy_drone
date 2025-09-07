using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecordsManager : MonoBehaviour {
    private static RecordsManager instance;

    private int[] topScores = new int[10];
    private bool showTop5 = true; // По умолчанию показываем 5 рекордов

    public Text[] recordTexts; // Массив текстовых элементов для отображения рекордов

    void Awake()
    {
        // Реализация синглтона
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeRecords();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeRecords()
    {
        // Инициализация нулевыми значениями
        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = 0;
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

        // Обновляем отображение после изменения рекордов
        UpdateRecordsDisplay();
    }

    public void UpdateRecordsDisplay()
    {
        if (recordTexts == null || recordTexts.Length == 0)
        {
            Debug.LogWarning("Текстовые элементы для отображения рекордов не назначены!");
            return;
        }

        // Определяем сколько рекордов показывать
        int recordsToShow = showTop5 ? 5 : 10;
        int displayCount = Mathf.Min(recordsToShow, recordTexts.Length, topScores.Length);

        // Отображаем рекорды
        for (int i = 0; i < displayCount; i++)
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

        // Скрываем неиспользуемые текстовые элементы
        for (int i = displayCount; i < recordTexts.Length; i++)
        {
            recordTexts[i].text = "";
        }
    }

    public void ChangeRecordsShown()
    {
        // Переключаем режим отображения
        showTop5 = !showTop5;
        UpdateRecordsDisplay();

        Debug.Log($"Режим отображения изменен: показывать {(showTop5 ? "5" : "10")} рекордов");
    }

    // Для получения текущих рекордов (например, для отображения в другом UI)
    public int[] GetTopScores()
    {
        return topScores.ToArray(); // Возвращаем копию массива
    }

    // Для получения текущего режима отображения
    public bool IsShowingTop5()
    {
        return showTop5;
    }
}