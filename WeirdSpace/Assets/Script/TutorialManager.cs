using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    public GameObject[] tutorialImages;
    public GameObject[] tutorialTexts;

    private bool isTutorialVisible = true;
    private int currentPage = 0;

    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    void Start()
    {        
        if (PlayerPrefs.GetInt("Tuto", 0) == 1)
        {
            tutorialPanel.SetActive(false);
            UpdateTutorialPage();
        }
        if (PlayerPrefs.GetInt("Tuto", 0) == 0)
        {
            tutorialPanel.SetActive(true);
            UpdateTutorialPage();
        }
    }

    public void OnQuestionMarkPressed()
    {
        if (!isTutorialVisible)
        {
            tutorialPanel.SetActive(true); // 그냥 바로 보여주기
            isTutorialVisible = true;
        }
    }

    public void OnExitButtonPressed()
    {
        if (isTutorialVisible)
        {
            tutorialPanel.SetActive(false);
            isTutorialVisible = false;

            currentPage = 0;               // 페이지 초기화
            UpdateTutorialPage();          // UI 업데이트
            PlayerPrefs.SetInt("Tuto", 1); // 데이터 저장 선언
            PlayerPrefs.Save();
        }
    }


    public void OnRightButtonPressed()
    {
        if (currentPage < tutorialImages.Length - 1)
        {
            currentPage++;
            UpdateTutorialPage();
        }
    }

    public void OnLeftButtonPressed()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateTutorialPage();
        }
    }

    void UpdateTutorialPage()
    {
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            bool isActive = (i == currentPage);
            tutorialImages[i].SetActive(isActive);
            tutorialTexts[i].SetActive(isActive);
        }

        leftArrowButton.SetActive(currentPage > 0);
        rightArrowButton.SetActive(currentPage < tutorialImages.Length - 1);
    }
}