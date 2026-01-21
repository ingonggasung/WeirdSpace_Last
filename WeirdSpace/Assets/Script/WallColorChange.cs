using UnityEngine;
using UnityEngine.Tilemaps;

public class WallColorChange : MonoBehaviour
{
    public Tilemap wallTilemap; 
    public Color normalColor = Color.white;
    public Color targetColor = Color.red; 
    public float colorChangeSpeed = 0.5f;

    private Transform player;
    private float lastPlayerX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (wallTilemap == null)
        {
            wallTilemap = GetComponent<Tilemap>();
        }

        lastPlayerX = player.position.x;
        wallTilemap.color = normalColor;
    }

    void Update()
    {
        float playerX = player.position.x;
        float t = Time.deltaTime * colorChangeSpeed;

        if (playerX < lastPlayerX) 
        {
            wallTilemap.color = Color.Lerp(wallTilemap.color, targetColor, t);
        }
        else if (playerX > lastPlayerX)
        {
            wallTilemap.color = Color.Lerp(wallTilemap.color, normalColor, t);
        }

        lastPlayerX = playerX;
    }
}
