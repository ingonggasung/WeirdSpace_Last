using UnityEngine;

public class PlayerSkinSetup : MonoBehaviour
{
    public Sprite[] charSprites; // 캐릭터 스프라이트 배열 (인스펙터에서 설정)
    public RuntimeAnimatorController[] charControllers; // 캐릭터 애니메이터 컨트롤러 배열 (인스펙터에서 설정)

    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;

    void Awake()
    {
        // Player의 SpriteRenderer와 Animator 컴포넌트 찾기
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        // PlayerPrefs에서 저장된 캐릭터 인덱스 가져오기
        int savedIndex = PlayerPrefs.GetInt("Character", 0);
        Debug.Log(savedIndex);
        // 캐릭터 설정
        ApplySkin(savedIndex);
    }

    private void ApplySkin(int index)
    {
        // 유효한 인덱스인지 확인
        if (index < 0 || index >= charSprites.Length || index >= charControllers.Length)
        {
            Debug.LogError("Invalid character index in PlayerPrefs!");
            return;
        }

        // 스프라이트와 애니메이터 컨트롤러 적용
        playerSpriteRenderer.sprite = charSprites[index];
        playerAnimator.runtimeAnimatorController = charControllers[index];

        Debug.Log($"Player skin applied: Sprite = {charSprites[index].name}, Controller = {charControllers[index].name}");
    }
}
