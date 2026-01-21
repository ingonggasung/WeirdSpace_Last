using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsManager_Choice : MonoBehaviour
{
    public GameObject pausePanel;       // 일시정지 메뉴 패널
    public GameObject tutorialPanel;    // 튜토리얼 패널

    void Start()
    {
        pausePanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    // 일시정지 메뉴 열기
    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    // 계속하기 버튼
    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    // 튜토리얼 열기
    public void OnTutorialButtonClicked()
    {
        tutorialPanel.SetActive(true);
    }

    // 튜토리얼 닫기
    public void OnCloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    // 종료 버튼 누르면 즉시 종료
    public void OnStopButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
