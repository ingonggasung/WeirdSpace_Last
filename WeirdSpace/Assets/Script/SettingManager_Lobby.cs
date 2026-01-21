using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager_Lobby : MonoBehaviour
{
    public static SettingManager_Lobby Instance;
    public GameObject settingsPanel;

    private Slider volumeSlider;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var allTransforms = GameObject.FindObjectsByType<Transform>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Transform t in allTransforms)
        {
            if (t.name == "SettingPanel")
            {
                settingsPanel = t.gameObject;
            }

            if (t.name == "Stop")
            {
                var btn = t.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(ShowSettings);
            }

            if (t.name == "ContinueButton")
            {
                var btn = t.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(HideSettings);
            }

            if (t.name == "StopButton")
            {
                var btn = t.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(GoToChoiceScene);
            }

            if (t.name == "TutorialButton")
            {
                var btn = t.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(HideSettings);
            }

            if (t.name == "ExitButton")
            {
                var btn = t.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(HideSettings);
            }
        }
    }

    void DestroyPersistentObjects()
    {
        // 삭제할 태그 목록
        string[] tagsToDestroy = { "Stop", "settingpanel" };

        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
    }

    void DestroyPersistentObjects2()
    {
        // 삭제할 태그 목록
        string[] tagsToDestroy = { "Player", "MainCamera", "GameManeger", "Button" };

        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
    }

    public void ShowSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }


    public void GoToChoiceScene()
    {
        SceneManager.LoadScene("Choice");
        DestroyPersistentObjects();
        DestroyPersistentObjects2();
    }
}
