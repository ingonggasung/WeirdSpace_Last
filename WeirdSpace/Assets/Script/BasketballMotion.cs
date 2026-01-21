using UnityEngine;

public class BasketballMotion : MonoBehaviour
{
    public Transform targetPoint;      // Where the ball will land (left-bottom)
    public float jumpHeight = 3f;      // How high the ball jumps
    public float moveDuration = 2f;    // Total movement time

    private bool isTriggered = false;
    private Vector3 startPoint;
    private float elapsed = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            startPoint = transform.position;
        }
    }

    void Update()
    {
        if (isTriggered && elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            // Parabola movement
            Vector3 peak = (startPoint + targetPoint.position) / 2 + Vector3.up * jumpHeight;
            Vector3 m1 = Vector3.Lerp(startPoint, peak, t);
            Vector3 m2 = Vector3.Lerp(peak, targetPoint.position, t);
            transform.position = Vector3.Lerp(m1, m2, t);
        }
    }
}
