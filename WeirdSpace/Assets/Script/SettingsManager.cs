using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public GameObject settingsPanel1;

    private Slider volumeSlider;
    private const string VolumePrefKey = "Volume";

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
        PlayerPrefs.SetInt("StagePage", 0);
        PlayerPrefs.Save();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var allTransforms = GameObject.FindObjectsByType<Transform>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Transform t in allTransforms)
        {
            if (t.name == "SettingPanel1")
            {
                settingsPanel1 = t.gameObject;
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

            // 🔊 볼륨 슬라이더 연결
            if (t.name == "VolumeSlider")
            {
                volumeSlider = t.GetComponent<Slider>();
                if (volumeSlider != null)
                {
                    // 저장된 값 불러오기
                    float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
                    volumeSlider.value = savedVolume;
                    AudioListener.volume = savedVolume;

                    // 리스너 연결
                    volumeSlider.onValueChanged.RemoveAllListeners();
                    volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
                }
            }
        }
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    void DestroyPersistentObjects()
    {
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
        if (settingsPanel1 != null)
            settingsPanel1.SetActive(true);
    }

    public void HideSettings()
    {
        if (settingsPanel1 != null)
            settingsPanel1.SetActive(false);
    }

    public void GoToChoiceScene()
    {
        GameObject tutorialPanel = GameObject.Find("TutorialPanel");
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
        SceneManager.LoadScene("Choice");
        DestroyPersistentObjects();
        DestroyPersistentObjects2();
    }
}
