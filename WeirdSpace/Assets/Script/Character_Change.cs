using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Change : MonoBehaviour
{
    public GameObject[] Char;
    public GameObject priceButton;   // price_btn 버튼 오브젝트
    public GameObject choiceButton;  // choice_btn 버튼 오브젝트

    private int currentIndex = 0;

    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("Character", 0);
        ShowCharacter(currentIndex);
    }

    public void Home_Btn()
    {
        SceneManager.LoadScene("Choice");
    }
    public void Right_Button()
    {
        currentIndex = (currentIndex + 1) % Char.Length;
        ShowCharacter(currentIndex);
    }

    public void Left_Button()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = Char.Length - 1;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        for (int i = 0; i < Char.Length; i++)
        {
            Char[i].SetActive(i == index);
        }

        bool isUnlocked = IsCharacterUnlocked(index);
        int selectedCharacter = PlayerPrefs.GetInt("Character", 0);

        // 기본적으로 버튼 전부 비활성화
        priceButton.SetActive(false);
        choiceButton.SetActive(false);

        if (!isUnlocked)
        {
            // 아직 구매하지 않은 캐릭터
            priceButton.SetActive(true);
        }
        else if (index != selectedCharacter)
        {
            // 구매했고, 현재 선택된 캐릭터가 아님
            choiceButton.SetActive(true);
        }
        // 선택된 캐릭터일 경우 둘 다 false 유지됨
    }

    public void choice_btn()
    {
        if (IsCharacterUnlocked(currentIndex))
        {
            PlayerPrefs.SetInt("Character", currentIndex);
            PlayerPrefs.Save();
            Debug.Log($"캐릭터 {currentIndex} 선택됨");

            ShowCharacter(currentIndex); // 버튼 상태 다시 갱신
        }
    }

    public void price_btn()
    {
        if (IsCharacterUnlocked(currentIndex))
        {
            Debug.Log("이미 구매한 캐릭터입니다.");
            return;
        }

        // ⚠ PlayerPrefs 대신 CoinManager에서 직접 가져오기
        int coin = CoinManager.Instance != null ? CoinManager.Instance.coin : 0;

        if (coin < 100)
        {
            Debug.Log("코인이 부족합니다. 최소 100 코인이 필요합니다.");
            return;
        }

        // 코인 차감
        CoinManager.Instance.coin -= 100;
        CoinManager.Instance.SaveCoin();
        CoinManager.Instance.LoadCoin();
        // 구매 처리
        UnlockCharacter(currentIndex);
        PlayerPrefs.SetInt("Character", currentIndex);
        PlayerPrefs.Save();

        Debug.Log($"캐릭터 {currentIndex} 구매 완료! 남은 코인: {CoinManager.Instance.coin}");

        ShowCharacter(currentIndex); // 버튼 상태 갱신
    }


    private bool IsCharacterUnlocked(int index)
    {
        return PlayerPrefs.GetInt($"CharacterUnlocked_{index}", index == 0 ? 1 : 0) == 1;
    }

    private void UnlockCharacter(int index)
    {
        PlayerPrefs.SetInt($"CharacterUnlocked_{index}", 1);
    }
}
