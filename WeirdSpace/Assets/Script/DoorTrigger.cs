using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "LivingClear";

    private void OnTriggerEnter2D(Collider2D other)
    {
        int index = PlayerPrefs.GetInt("LivingRoomClear", 0);

        if (other.CompareTag("Player"))
        {
            StartCoroutine(CleanupAndLoadScene());
        }
    }

    IEnumerator CleanupAndLoadScene()
    {
        string[] tagsToDestroy = { "Player", "GameManeger", "MainCamera", "Button", "Stop", "settingpanel" };

        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
        // 스테이지 클리어 상태 저장
        PlayerPrefs.SetInt("LivingRoomClear", 1);
        PlayerPrefs.Save();

        yield return null;

        SceneManager.LoadScene(nextSceneName);
    }
}
