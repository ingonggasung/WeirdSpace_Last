using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardAds_YesNo : MonoBehaviour
{
    public GameObject Panel;
    void Start()
    {
        
    }

    public void PlusBtn()
    {
        Debug.Log("버튼 인식");
        Panel.SetActive(true);
    }
    void Update()
    {   
        
    }
}
