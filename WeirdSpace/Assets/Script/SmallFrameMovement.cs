using UnityEngine;

public class SmallFrameScaleAndMove : MonoBehaviour
{
    public float floatSpeed = 2.0f;  // 이동 속도
    public float floatHeight = 0.8f; // 위아래 이동 범위
    public float floatWidth = 0.2f;  // 좌우 이동 범위
    private bool isPlayerNear = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            float newY = startPos.y + Mathf.PingPong(Time.time * floatSpeed, floatHeight * 2) - floatHeight;
            float newX = startPos.x + Mathf.PingPong(Time.time * floatSpeed, floatWidth * 2) - floatWidth;

            transform.position = new Vector3(newX, newY, startPos.z);
        }
        else
        {
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
