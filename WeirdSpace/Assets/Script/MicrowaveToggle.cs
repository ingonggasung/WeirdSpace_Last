using UnityEngine;

public class MicrowaveToggle : MonoBehaviour
{
    public GameObject otherMicrowave;  // 상대방 오브젝트 (열린 상태 or 닫힌 상태)

    public float toggleInterval = 1.5f; // 전환 주기
    private float timer = 0f;

    void Start()
    {
        if (otherMicrowave == null)
        {
            Debug.LogError("다른 전자레인지 오브젝트가 연결되지 않았습니다!");
            return;
        }

        // 현재 오브젝트는 활성화 상태, 상대는 비활성화 상태로 시작
        gameObject.SetActive(true);
        otherMicrowave.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= toggleInterval)
        {
            timer = 0f;

            // 자기 자신을 비활성화하고, 다른 걸 활성화
            gameObject.SetActive(false);
            otherMicrowave.SetActive(true);
        }
    }
}
