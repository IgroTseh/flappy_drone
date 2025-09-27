using UnityEngine;

public class QuitManager : MonoBehaviour {
    // ����� ��� ������ ������
    public void QuitGame()
    {
#if UNITY_ANDROID
            // ���������� ��������������� ����� ��� Android
            MoveAppToBackground();
#else
        // ��� ������ �������� (Windows, Mac) ���������� ����������� �����
        Application.Quit();
#endif
    }

    private void MoveAppToBackground()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }
}