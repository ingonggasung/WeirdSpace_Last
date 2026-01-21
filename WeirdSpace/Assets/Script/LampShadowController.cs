using UnityEngine;
using TMPro;


public class LampShadowController : MonoBehaviour
{
    [Header("Normal Shadows")]
    [SerializeField] private GameObject[] normalShadows;

    [Header("All Shadows")]
    [SerializeField] private GameObject[] allShadows;

    [Header("First Text (TMP)")]
    [SerializeField] private TextMeshProUGUI firstText;

    [Header("Second Text (TMP)")]
    [SerializeField] private TextMeshProUGUI secondText;

    [Header("Initial Lamp State (true = on)")]
    [SerializeField] private bool isLampOn = true;

    [Header("Flicker Interval Range (seconds)")]
    [SerializeField] private float minWait = 0.05f;
    [SerializeField] private float maxWait = 2f;

    [Header("Flicker Duration (time lamp stays off)")]
    [SerializeField] private float flickerDuration = 0.3f;

    private void Start()
    {
        UpdateLampState(isLampOn);
        StartCoroutine(FlickerRoutine());
    }

    private void UpdateLampState(bool lampOn)
    {
        // Enable or disable normal shadows
        foreach (var shadow in normalShadows)
        {
            shadow.SetActive(lampOn);
        }

        // Enable or disable all shadows
        foreach (var shadow in allShadows)
        {
            shadow.SetActive(!lampOn);
        }

        // Show or hide the first text
        if (firstText != null)
        {
            firstText.gameObject.SetActive(lampOn);
        }

        // Show or hide the second text
        if (secondText != null)
        {
            secondText.gameObject.SetActive(lampOn);
        }

        isLampOn = lampOn;
    }

    private System.Collections.IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            if (isLampOn)
            {
                UpdateLampState(false);
                yield return new WaitForSeconds(flickerDuration);
                UpdateLampState(true);
            }
        }
    }
}
