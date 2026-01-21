using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] private Image starImage_L;
    [SerializeField] private Image starImage_B;
    [SerializeField] private Image starImage_Kid;

    void Awake()
    {
        // 무조건 먼저 알파값 0으로 초기화
        SetAlpha(starImage_L, 0f);
        SetAlpha(starImage_B, 0f);
        SetAlpha(starImage_Kid, 0f);
    }

    void Start()
    {
        // 조건 만족 시 1로 변경
        if (PlayerPrefs.GetInt("LivingRoomClear", 0) == 1)
        {
            SetAlpha(starImage_L, 1f);
        }
        if (PlayerPrefs.GetInt("BathroomClear", 0) == 1)
        {
            SetAlpha(starImage_B, 1f);
        }
        if (PlayerPrefs.GetInt("KidroomClear", 0) == 1)
        {
            SetAlpha(starImage_Kid, 1f);
        }
    }

    void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
