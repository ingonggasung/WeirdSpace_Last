using UnityEngine;
using UnityEngine.SceneManagement;

public class Tele : MonoBehaviour
{
    // 스테이지 번호
    int stageIndex;

    // 트리거가 한 번만 발생하도록 제어하는 플래그
    bool hasTeleported = false;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject camera = GameObject.FindWithTag("MainCamera");        
        if (player != null)
        {
            // 중복 플레이어 검사 및 제거
            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                Destroy(player);
                return;
            }
            DontDestroyOnLoad(player);
        }
        DontDestroyOnLoad(camera);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 한 번 텔레포트한 후에는 더 이상 트리거되지 않도록 제어
        if (hasTeleported) return;

        if (other.CompareTag("Player"))
        {
            // 트리거 발생 후 더 이상 실행되지 않도록 플래그 설정
            hasTeleported = true;

            // 확률 조정
            int randomIndex = Random.Range(0, 30);

            if (0 <= randomIndex && randomIndex < 10)
            {
                stageIndex = 1;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (10 <= randomIndex && randomIndex < 20)
            {
                stageIndex = 2;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (20 <= randomIndex && randomIndex < 30)
            {
                stageIndex = 3;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (30 <= randomIndex && randomIndex < 40)
            {
                stageIndex = 4;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (40 <= randomIndex && randomIndex < 50)
            {
                stageIndex = 5;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (50 <= randomIndex && randomIndex < 60)
            {
                stageIndex = 6;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (60 <= randomIndex && randomIndex < 70)
            {
                stageIndex = 7;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (70 <= randomIndex && randomIndex < 80)
            {
                stageIndex = 8;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (80 <= randomIndex && randomIndex < 90)
            {
                stageIndex = 9;
                Debug.Log("stageIndex : " + stageIndex);
            }
            else if (90 <= randomIndex && randomIndex < 100)
            {
                stageIndex = 10;
                Debug.Log("stageIndex : " + stageIndex);
            }

            // 씬이 로드되면 플레이어를 텔레포트할 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 스테이지로 씬 전환
            SceneManager.LoadScene("Stage" + stageIndex);
        }
    }

    // 씬이 로드된 후 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // "Player" 태그가 붙은 오브젝트(플레이어)를 찾음
        GameObject player = GameObject.FindWithTag("Player");
        GameObject camera = GameObject.FindWithTag("MainCamera");

        if (player != null)
        {
            // 현재 플레이어의 x좌표를 가져옴
            float playerX = player.transform.position.x;
            float cameraX = camera.transform.position.x;

            // 텔레포트 당시 x좌표가 15보다 작으면 +2.5, 0 이상이면 -2.5로 이동
            if (playerX < 15)
            {
                player.transform.position = new Vector3(playerX + 2.5f, player.transform.position.y, player.transform.position.z);
                //camera.transform.position = new Vector3(cameraX + 2.5f, camera.transform.position.y, camera.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(playerX - 2.5f, player.transform.position.y, player.transform.position.z);
                //camera.transform.position = new Vector3(cameraX - 2.5f, camera.transform.position.y, camera.transform.position.z);
            }
        }

        // 이벤트 등록 해제 (다음 씬 로드 시 다시 등록될 수 있음)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

