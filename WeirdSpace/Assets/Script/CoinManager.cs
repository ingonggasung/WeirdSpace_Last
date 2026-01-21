using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    public int coin;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지되도록
            LoadCoin(); // 저장된 코인 불러오기
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    void OnApplicationQuit()
    {
        SaveCoin(); // 앱 종료 시 코인 저장
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coin", coin); // 기존 코드 유지
        PlayerPrefs.Save();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        coin = PlayerPrefs.GetInt("Coin", 0); // 없으면 기본값 0
    }
}