using UnityEngine;

public class PPmanager : MonoBehaviour {
    public static PPmanager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.LoadAll();
        }
        else
        {
            Debug.LogWarning("SaveManager not found, PPmanager can't load prefs");
        }
    }

    void OnApplicationQuit()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveAll();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveAll();
        }
    }

    public void UpdatePPAll()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager not found, can't update all PP");
            return;
        }

        SaveManager.Instance.SaveAll();
        PlayerPrefs.Save();
    }
}
