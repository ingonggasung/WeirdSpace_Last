using UnityEngine;

public class LawnMowerMove : MonoBehaviour
{
    public float leftTargetX = 14f;       // Destination X when moving left (mirrored)
    public float rightTargetX = 16.5f;    // Destination X when moving right (default)
    public float speed = 15f;

    private bool movingRight = true;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // Save original scale
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.position += new Vector3(move, 0, 0);

            if (transform.position.x >= rightTargetX)
            {
                movingRight = false;

                // Flip horizontally and adjust position after flipping
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                transform.position = new Vector3(52f, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position -= new Vector3(move, 0, 0);

            if (transform.position.x <= leftTargetX)
            {
                movingRight = true;

                // Restore original scale and adjust position after flipping back
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                transform.position = new Vector3(-23.5f, transform.position.y, transform.position.z);
            }
        }
    }
}
