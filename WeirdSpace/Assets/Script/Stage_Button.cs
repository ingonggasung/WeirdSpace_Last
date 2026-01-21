using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Button : MonoBehaviour
{
    public Button livingRoomButton;
    public Button kitchenButton;
    public Button bathButton;
    public Button garButton;
    public Button kidButton;
    //public Button parentsButton;

    public GameObject[] Page1;
    public GameObject[] Page2;
    public GameObject[] Page3;

    public GameObject bathroomConfirmPanel;
    public GameObject garageConfirmPanel;
    public GameObject Bath_Lock;
    public GameObject Garage_Lock;

    public GameObject kidroomConfirmPanel;
    //public GameObject parentsroomConfirmPanel;
    public GameObject Kid_Lock;
    //public GameObject Parents_Lock;

    private int stageint;
    private int stagepage = 3; // Page1, Page2, Page3

    private bool bathUnlocked = false;
    private bool garageUnlocked = false;
    private bool kidUnlocked = false;
    //private bool parentsUnlocked = false;

    void Start()
    {
        stageint = PlayerPrefs.GetInt("StagePage");
        // 구매 여부 불러오기
        bathUnlocked = PlayerPrefs.GetInt("Bought_Bathroom", 0) == 1;
        garageUnlocked = PlayerPrefs.GetInt("Bought_Garage", 0) == 1;
        kidUnlocked = PlayerPrefs.GetInt("Bought_Kidroom", 0) == 1;
        //parentsUnlocked = PlayerPrefs.GetInt("Bought_Parents", 0) == 1;

        // 락 표시 반영
        Bath_Lock.SetActive(!bathUnlocked);
        Garage_Lock.SetActive(!garageUnlocked);
        Kid_Lock.SetActive(!kidUnlocked);
        //Parents_Lock.SetActive(!parentsUnlocked);

        // 버튼 클릭 이벤트 등록
        livingRoomButton.onClick.AddListener(() => LoadScene("livingroom"));
        kitchenButton.onClick.AddListener(() => LoadScene("Kitchen"));

        bathButton.onClick.AddListener(() =>
        {
            if (bathUnlocked) 
            { 
                LoadScene("Bathroom");
                PlayerPrefs.SetInt("StagePage", stageint);
                PlayerPrefs.Save();
            }
            else
                bathroomConfirmPanel.SetActive(true);
        });

        garButton.onClick.AddListener(() =>
        {
            if (garageUnlocked)
            {
                LoadScene("Garage");
                PlayerPrefs.SetInt("StagePage", stageint);
                PlayerPrefs.Save();
            }
            else
                garageConfirmPanel.SetActive(true);
        });

        kidButton.onClick.AddListener(() =>
        {
            if (kidUnlocked)
            {
                LoadScene("Kidroom");
                PlayerPrefs.SetInt("StagePage", stageint);
                PlayerPrefs.Save();
            }
            else
                kidroomConfirmPanel.SetActive(true);
        });

        //parentsButton.onClick.AddListener(() =>
        //{
        //    if (parentsUnlocked)
        //    {
        //        LoadScene("Parentsroom");
        //        PlayerPrefs.SetInt("StagePage", stageint);
        //        PlayerPrefs.Save();
        //    }
        //    else
        //        parentsroomConfirmPanel.SetActive(true);
        //});

        //// 이전 씬이 Main이면 Page1만 보여주기
        //string prevScene = PlayerPrefs.GetString("PreviousScene", "");
        //if (prevScene == "Main")
        //{
        //    stageint = 0; // Page1
        //}
        //else
        //{
        //    stageint = PlayerPrefs.GetInt("StagePage", 0);
        //}

        StageShow(stageint);
    }

    public void Right_Button()
    {
        stageint++;
        if (stageint >= stagepage) stageint = stagepage - 1;
        StageShow(stageint);
    }

    public void Left_Button()
    {
        stageint--;
        if (stageint < 0) stageint = 0;
        StageShow(stageint);
    }

    private void StageShow(int stagenumber)
    {
        foreach (GameObject obj in Page1) obj.SetActive(false);
        foreach (GameObject obj in Page2) obj.SetActive(false);        
        foreach (GameObject obj in Page3) obj.SetActive(false);        
        //foreach (GameObject obj in Page3) obj.SetActive(false);

        switch (stagenumber)
        {
            case 0:
                foreach (GameObject obj in Page1) obj.SetActive(true);
                break;
            case 1:
                foreach (GameObject obj in Page2) obj.SetActive(true);
                break;
            case 2:
                foreach (GameObject obj in Page3) obj.SetActive(true);
                break;
        }
    }

    void LoadScene(string sceneName)
    {
        //// 이전 씬 저장
        //PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        // 현재 페이지 저장
        PlayerPrefs.SetInt("StagePage", stageint);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }

    // ✅ Bathroom 구매 확인
    public void ConfirmBuyBathroom()
    {
        if (CoinManager.Instance.coin >= 40)
        {
            CoinManager.Instance.AddCoin(-40);
            PlayerPrefs.SetInt("Bought_Bathroom", 1);
            PlayerPrefs.Save();

            bathUnlocked = true;
            Bath_Lock.SetActive(false);
            bathroomConfirmPanel.SetActive(false);
            Debug.Log("Bathroom 구매 완료!");
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }

    // ✅ Garage 구매 확인
    public void ConfirmBuyGarage()
    {
        if (CoinManager.Instance.coin >= 40)
        {
            CoinManager.Instance.AddCoin(-40);
            PlayerPrefs.SetInt("Bought_Garage", 1);
            PlayerPrefs.Save();

            garageUnlocked = true;
            Garage_Lock.SetActive(false);
            garageConfirmPanel.SetActive(false);
            Debug.Log("Garage 구매 완료!");
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }

    // ✅ Kidroom 구매 확인
    public void ConfirmBuyKidroom()
    {
        if (CoinManager.Instance.coin >= 40)
        {
            CoinManager.Instance.AddCoin(-40);
            PlayerPrefs.SetInt("Bought_Kidroom", 1);
            PlayerPrefs.Save();

            kidUnlocked = true;
            Kid_Lock.SetActive(false);
            kidroomConfirmPanel.SetActive(false);
            Debug.Log("Kid 구매 완료!");
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }

    // ✅ Parentsroom 구매 확인
    //public void ConfirmBuyParentsroom()
    //{
    //    if (CoinManager.Instance.coin >= 40)
    //    {
    //        CoinManager.Instance.AddCoin(-40);
    //        PlayerPrefs.SetInt("Bought_Parents", 1);
    //        PlayerPrefs.Save();

    //        parentsUnlocked = true;
    //        Parents_Lock.SetActive(false);
    //        parentsroomConfirmPanel.SetActive(false);
    //        Debug.Log("Parentsroom 구매 완료!");
    //    }
    //    else
    //    {
    //        Debug.Log("코인이 부족합니다.");
    //    }
    //}

    // ❌ 구매 취소
    public void CancelBuyBathroom()
    {
        bathroomConfirmPanel.SetActive(false);
    }

    public void CancelBuyGarage()
    {
        garageConfirmPanel.SetActive(false);
    }

    public void CancelBuyKidroom()
    {
        kidroomConfirmPanel.SetActive(false);
    }

    //public void CancelBuyParentsroom()
    //{
    //    parentsroomConfirmPanel.SetActive(false);
    //}
}
