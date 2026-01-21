using UnityEngine;
using UnityEngine.UI;

public class StarDisplay_K : MonoBehaviour
{
    [SerializeField] private Image starImage_K;
    [SerializeField] private Image starImage_G;
    [SerializeField] private Image starImage_P;

    void Awake()
    {
        // 무조건 먼저 알파값 0으로 초기화
        SetAlpha(starImage_K, 0f);
        SetAlpha(starImage_G, 0f);
        SetAlpha(starImage_P, 0f);
    }

    void Start()
    {
        // 조건 만족 시 1로 변경
        if (PlayerPrefs.GetInt("KitchenClear", 0) == 1)
        {
            SetAlpha(starImage_K, 1f);
        }
        if (PlayerPrefs.GetInt("GarageClear", 0) == 1)
        {
            SetAlpha(starImage_G, 1f);
        }
        if (PlayerPrefs.GetInt("ParentsroomClear", 0) == 1)
        {
            SetAlpha(starImage_P, 1f);
        }
    }

    void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
