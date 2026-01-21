using UnityEngine;

public class CartMove : MonoBehaviour
{
    public float rightTargetX = 46f;       // 좌우반전된 상태에서 오른쪽 이동 끝 위치
    public float leftTargetX = -16f;    // 원래 방향에서 왼쪽 이동 끝 위치
    public float speed = 15f;

    private bool movingLeft = true;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        if (movingLeft)
        {
            transform.position -= new Vector3(move, 0, 0);

            if (transform.position.x <= leftTargetX)
            {
                movingLeft = false;

                // 반전 + 위치 재조정
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                transform.position = new Vector3(10f, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position += new Vector3(move, 0, 0);

            if (transform.position.x >= rightTargetX)
            {
                movingLeft = true;

                // 다시 원래 방향 + 위치 재조정
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
                transform.position = new Vector3(20f, transform.position.y, transform.position.z);
            }
        }
    }
}
