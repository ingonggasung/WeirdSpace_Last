using UnityEngine;

public class GarShadowSwitcher : MonoBehaviour
{
    public GameObject shadowA; // Original "Shadow"
    public GameObject shadowB; // "Shadow(1)"
    public float switchInterval = 1f;

    private float timer = 0f;
    private bool toggle = true;

    void Start()
    {
        // Ensure only one shadow is active at start
        shadowA.SetActive(true);
        shadowB.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            toggle = !toggle;
            shadowA.SetActive(toggle);
            shadowB.SetActive(!toggle);
            timer = 0f;
        }
    }
}
