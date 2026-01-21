using UnityEngine;

public class RollerMove : MonoBehaviour
{
    public float upY = 2f;
    public float downY = -2f;
    public float speed = 2f;

    private Vector3 startPos;
    private float currentTargetY;
    private bool isPlayerInside = false;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
        currentTargetY = upY;
    }

    void Update()
    {
        if (!isPlayerInside) return;

        // Move up or down while player is inside
        Vector3 targetPos = new Vector3(startPos.x, currentTargetY, startPos.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.y, currentTargetY))
        {
            currentTargetY = (currentTargetY == upY) ? downY : upY;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
