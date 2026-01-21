using UnityEngine;
using UnityEngine.Tilemaps;

public class StoveColorChanger : MonoBehaviour
{
    public Color colorA = Color.white;
    public Color colorB = new Color(1f, 0.41f, 0.41f); // FF6969

    private Tilemap tilemap;
    private float duration = 1.5f; // 반 주기 (전체 주기 3초)

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Tilemap 컴포넌트가 없습니다!");
        }
    }

    void Update()
    {
        if (tilemap != null)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            tilemap.color = Color.Lerp(colorA, colorB, t);
        }
    }
}
