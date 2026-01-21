using UnityEngine;

public class Turn : MonoBehaviour
{
    public float moveDistance = 0.5f;    // 좌우/상하 이동 거리
    public float moveSpeed = 2.0f;       // 이동 속도 (값을 줄이면 느리게 걷는 느낌)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        float y = Mathf.Cos(Time.time * moveSpeed) * moveDistance * 0.5f; // y 이동은 조금만

        transform.position = startPos + new Vector3(x, y, 0);
    }
}
