using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecordsManager : MonoBehaviour {
    private static RecordsManager instance;

    private int[] topScores = new int[10];
    private bool showTop5 = true; // �� ��������� ���������� 5 ��������

    public Text[] recordTexts; // ������ ��������� ��������� ��� ����������� ��������

    void Awake()
    {
        // ���������� ���������
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
        // ������������� �������� ����������
        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = 0;
        }
    }

    public void UpdateRecords(int newScore)
    {
        // ���������, �������� �� ����� ��������� ��������
        if (newScore <= topScores[topScores.Length - 1])
            return;

        // ������� ������� ��� �������
        for (int i = 0; i < topScores.Length; i++)
        {
            if (newScore > topScores[i])
            {
                // �������� ������� ���������� ����
                for (int j = topScores.Length - 1; j > i; j--)
                {
                    topScores[j] = topScores[j - 1];
                }

                // ��������� ����� ���������
                topScores[i] = newScore;
                break;
            }
        }

        // ��������� ����������� ����� ��������� ��������
        UpdateRecordsDisplay();
    }

    public void UpdateRecordsDisplay()
    {
        if (recordTexts == null || recordTexts.Length == 0)
        {
            Debug.LogWarning("��������� �������� ��� ����������� �������� �� ���������!");
            return;
        }

        // ���������� ������� �������� ����������
        int recordsToShow = showTop5 ? 5 : 10;
        int displayCount = Mathf.Min(recordsToShow, recordTexts.Length, topScores.Length);

        // ���������� �������
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

        // �������� �������������� ��������� ��������
        for (int i = displayCount; i < recordTexts.Length; i++)
        {
            recordTexts[i].text = "";
        }
    }

    public void ChangeRecordsShown()
    {
        // ����������� ����� �����������
        showTop5 = !showTop5;
        UpdateRecordsDisplay();

        Debug.Log($"����� ����������� �������: ���������� {(showTop5 ? "5" : "10")} ��������");
    }

    // ��� ��������� ������� �������� (��������, ��� ����������� � ������ UI)
    public int[] GetTopScores()
    {
        return topScores.ToArray(); // ���������� ����� �������
    }

    // ��� ��������� �������� ������ �����������
    public bool IsShowingTop5()
    {
        return showTop5;
    }
}