using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecordsManager : MonoBehaviour {
    private static RecordsManager instance;

    private int[] topScores = new int[6]; // 6 ��������

    private Text[] recordTexts = new Text[6]; // 6 ��������� ���������

    void Awake()
    {
        // ���������� ���������
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
        // ��������� ����������� �������� ������ ����
        UpdateRecordsDisplay();
    }

    private void InitializeRecords()
    {
        // ������������� �������� ����������
        for (int i = 0; i < topScores.Length; i++)
        {
            topScores[i] = 0;
        }
    }

    private void FindRecordTextsByTags()
    {
        // ������� ��������� �������� �� ����� Record0-Record5
        for (int i = 0; i < 6; i++)
        {
            string tagName = "Record" + i;
            GameObject textObject = GameObject.FindWithTag(tagName);

            if (textObject != null)
            {
                recordTexts[i] = textObject.GetComponent<Text>();
                if (recordTexts[i] != null)
                {
                    Debug.Log("������ ��������� ������� � �����: " + tagName);
                }
                else
                {
                    Debug.LogWarning("� ������� � ����� " + tagName + " ��� ���������� Text");
                }
            }
            else
            {
                Debug.LogWarning("�� ������ ������ � �����: " + tagName);
            }
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
    }

    private void UpdateRecordsDisplay()
    {
        // ���� ��������� �������� �� �������, �������� ����� ��
        if (recordTexts == null || recordTexts.Length == 0 || recordTexts[0] == null)
        {
            FindRecordTextsByTags();

            // ���� ����� ������ ��� ��� �� �������, �������
            if (recordTexts == null || recordTexts.Length == 0 || recordTexts[0] == null)
            {
                return;
            }
        }

        // ���������� ��� 6 ��������
        int displayCount = Mathf.Min(recordTexts.Length, topScores.Length);

        // ���������� �������
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

        // �������� �������������� ��������� �������� (���� �� ������ 6)
        for (int i = displayCount; i < recordTexts.Length; i++)
        {
            if (recordTexts[i] != null)
            {
                recordTexts[i].text = "";
            }
        }
    }

    // ��� ��������� ������� ��������
    public int[] GetTopScores()
    {
        return topScores.ToArray();
    }

    // ��� ��������� �������� (��� SaveManager)
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