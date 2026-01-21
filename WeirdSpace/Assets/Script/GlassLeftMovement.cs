using UnityEngine;

public class GlassLeftMovement : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // 이동 속도 (값을 키우면 더 빠름)
    public float floatHeight = 0.5f; // 위아래 이동 범위
    public float minY;

    private bool isPlayerNear = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; 
        minY = startPos.y; 
    }

    void Update()
    {
        if (isPlayerNear)
        {
            float newY = startPos.y + Mathf.PingPong(Time.time * floatSpeed, floatHeight * 2) - floatHeight;
            newY = Mathf.Clamp(newY, minY, startPos.y + floatHeight);

            transform.position = new Vector3(startPos.x, newY, startPos.z);
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
