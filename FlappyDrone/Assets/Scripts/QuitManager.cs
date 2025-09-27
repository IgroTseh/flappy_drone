using UnityEngine;

public class QuitManager : MonoBehaviour {
    // Метод для кнопки выхода
    public void QuitGame()
    {
#if UNITY_ANDROID
            // Используем рекомендованный метод для Android
            MoveAppToBackground();
#else
        // Для других платформ (Windows, Mac) используем стандартный выход
        Application.Quit();
#endif
    }

    private void MoveAppToBackground()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }
}