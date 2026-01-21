using UnityEngine;

public class BrushMove : MonoBehaviour
{
    public float amplitude = 0.5f;    
    public float speed = 1f;          
    private float startY;

    void Start()
    {
        startY = transform.position.y;  
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
