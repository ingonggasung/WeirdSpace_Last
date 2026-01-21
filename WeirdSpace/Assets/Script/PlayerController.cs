using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce;

    public AudioClip footstepClip; // 발소리 클립
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public GameObject leftButton;
    public GameObject rightButton;

    private float stepTimer = 0f;
    private float stepDelay = 0.5f; // 발소리 간격 (초)

    SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footstepClip;
        audioSource.loop = false; // 계속 반복되도록 설정
        jumpForce = 2.0f;
        moveLeft = false;
        moveRight = false;
    }

    public void PointerDownLeft() { moveLeft = true; }
    public void PointerUpLeft() { moveLeft = false; }
    public void PointerUpRight() { moveRight = false; }
    public void PointerDownRight() { moveRight = true; }

    private void MovementPlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
            animator.SetBool("walk", true);
            spriteRenderer.flipX = true; // 왼쪽 이동 시 반전
        }
        else if (moveRight)
        {
            horizontalMove = speed;
            animator.SetBool("walk", true);
            spriteRenderer.flipX = false; // 오른쪽 이동 시 원래 방향
        }
        else
        {
            horizontalMove = 0;
            animator.SetBool("walk", false);
        }
    }
    public void ResetMoveState()
    {
        moveLeft = false;
        moveRight = false;
        horizontalMove = 0f;
    }

    void Update()
    {
        MovementPlayer();
        Vector2 vector2 = new Vector2(horizontalMove, rb.linearVelocity.y);
        rb.linearVelocity = vector2;


        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        bool isMoving = isGrounded && Mathf.Abs(horizontalMove) > 0.1f;
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        // 발소리 처리
        if (isGrounded && Mathf.Abs(horizontalMove) > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                audioSource.PlayOneShot(footstepClip);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepDelay; // 멈추면 타이머 초기화
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }


    }


    public void SetButtonsActive(bool isActive)
    {
        if (leftButton != null) leftButton.SetActive(isActive);
        if (rightButton != null) rightButton.SetActive(isActive);
    }
}
