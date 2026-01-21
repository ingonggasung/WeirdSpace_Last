using UnityEngine;

public class Tutorial_Manager2 : MonoBehaviour
{
    public GameObject tutorialPanel;

    public GameObject[] tutorialImages;
    public GameObject[] tutorialTexts;

    private bool isTutorialVisible = false;
    private int currentPage = 0;

    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    void Start()
    {
        tutorialPanel.SetActive(false); // 처음엔 숨겨두기
        UpdateTutorialPage();
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
