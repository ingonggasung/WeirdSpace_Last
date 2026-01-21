using TMPro;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance { get; private set; }
    public int GameCount = 0;
    public TextMeshProUGUI score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 싱글톤이므로 중복된 인스턴스 파괴
        }
    }
    private void Update()
    {
        score.text = GameCount.ToString() + " / 8";
    }
}
