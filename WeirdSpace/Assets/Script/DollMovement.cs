using UnityEngine;

public class DollMovement : MonoBehaviour
{
    public float moveDistance = 0.6f; // 이동할 거리
    public float moveSpeed = 1.5f; // 이동 속도
    private Vector3 startPosition;
    private bool movingRight = true; // 오른쪽으로 이동 여부

    void Start()
    {
        startPosition = transform.position; // 시작 위치 저장
    }

    void Update()
    {
        // 현재 위치 계산
        float moveDirection = movingRight ? 1f : -1f;
        transform.position += new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        // 최대 이동 거리 도달 시 방향 전환
        if (movingRight && transform.position.x >= startPosition.x + moveDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= startPosition.x - moveDistance)
        {
            movingRight = true;
        }
    }
}
