using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeOnceOn : MonoBehaviour
{
    public Tilemap tilemapA;
    public Tilemap tilemapB;
    public float fadeSpeed = 1.5f;

    private bool isPlayerNear = false;
    private bool hasSwitched = false;

    void Start()
    {
        SetTilemapAlpha(tilemapA, 1f); // A 보이게
        SetTilemapAlpha(tilemapB, 0f); // B 숨김
    }

    void Update()
    {
        if (!isPlayerNear || hasSwitched) return;

        // A는 사라지고 B는 나타남
        FadeOut(tilemapA);
        FadeIn(tilemapB);

        if (IsFullyFaded(tilemapA) && IsFullyVisible(tilemapB))
        {
            hasSwitched = true; // 한 번만 전환되게 설정
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
}
