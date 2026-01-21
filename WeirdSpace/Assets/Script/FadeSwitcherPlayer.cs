using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeSwitcherPlayer : MonoBehaviour
{
    public Tilemap tilemapA;
    public Tilemap tilemapB;
    public float fadeSpeed = 10.0f;
    public float pauseDuration;

    private bool fadingToB = true;
    private float pauseTimer = 0f;
    private bool isPlayerNear = false;

    void Start()
    {
        SetTilemapAlpha(tilemapA, 1f); // A는 처음 보이게
        SetTilemapAlpha(tilemapB, 0f); // B는 숨기기
    }

    void Update()
    {
        if (!isPlayerNear) return;

        pauseDuration = Random.Range(0.0001f, 0.6f);
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        if (fadingToB)
        {
            FadeOut(tilemapA);
            FadeIn(tilemapB);

            if (IsFullyFaded(tilemapA) && IsFullyVisible(tilemapB))
            {
                fadingToB = false;
                pauseTimer = pauseDuration;
            }
        }
        else
        {
            FadeOut(tilemapB);
            FadeIn(tilemapA);

            if (IsFullyFaded(tilemapB) && IsFullyVisible(tilemapA))
            {
                fadingToB = true;
                pauseTimer = pauseDuration;
            }
        }
    }

    private void FadeIn(Tilemap tm)
    {
        Color c = tm.color;
        c.a = Mathf.MoveTowards(c.a, 1f, fadeSpeed * Time.deltaTime);
        tm.color = c;
    }

    private void FadeOut(Tilemap tm)
    {
        Color c = tm.color;
        c.a = Mathf.MoveTowards(c.a, 0f, fadeSpeed * Time.deltaTime);
        tm.color = c;
    }

    private void SetTilemapAlpha(Tilemap tm, float alpha)
    {
        Color c = tm.color;
        c.a = alpha;
        tm.color = c;
    }

    private bool IsFullyFaded(Tilemap tm)
    {
        return tm.color.a <= 0.01f;
    }

    private bool IsFullyVisible(Tilemap tm)
    {
        return tm.color.a >= 0.99f;
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
