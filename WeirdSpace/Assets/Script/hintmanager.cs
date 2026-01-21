using UnityEngine;

public class hintmanager : MonoBehaviour
{
    public GameObject HintPannel;
    public GameObject AdsPannel;
    void Start()
    {
        
    }
    public void CloseHintPannel()
    {
        HintPannel.SetActive(false);
        AdsPannel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
