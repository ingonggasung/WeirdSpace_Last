using UnityEngine;
using UnityEngine.UI; // 버튼에 필요
#if UNITY_EDITOR
using UnityEditor; 
#endif

public class ExitGameManager : MonoBehaviour
{
    public GameObject exitPanel; 

    public void OnExitButtonClicked()
    {
        exitPanel.SetActive(true); 
    }

    public void OnYesButtonClicked()
    {
        //Application.Quit();
        // 게임 종료
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif
    }

    public void OnNoButtonClicked()
    {
        exitPanel.SetActive(false); 
    }
}
