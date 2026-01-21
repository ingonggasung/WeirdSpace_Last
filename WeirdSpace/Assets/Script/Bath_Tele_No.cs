using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bath_Tele_No : MonoBehaviour
{
    int stageIndex;
    bool hasTeleported = false;
    public Image fadeImage; // 반드시 에디터에서 할당하거나 아래처럼 자동 할당

    void Start()
    {
        // fadeImage가 안 할당되어 있으면 자동으로 찾기
        if (fadeImage == null)
        {
            GameObject fadeObj = GameObject.Find("FadeImage"); // 오브젝트 이름에 맞게 수정
            if (fadeObj != null)
            {
                fadeImage = fadeObj.GetComponent<Image>();
            }
        }

        // 그래도 못 찾으면 오류 출력하고 중단
        if (fadeImage == null)
        {
            Debug.LogError("fadeImage가 설정되지 않았습니다. 에디터에서 Image를 할당하거나 이름을 확인하세요.");
            return;
        }

        // 화면을 처음 어둡게 설정
        fadeImage.color = new Color(0, 0, 0, 1);

        // 페이드 아웃 시작
        StartCoroutine(FadeOutAtStart());

        // DontDestroyOnLoad 처리
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        GameObject[] managers = GameObject.FindGameObjectsWithTag("GameManeger");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button"); // UI는 통째로 처리

        foreach (GameObject p in players)
        {
            p.transform.parent = null;
            DontDestroyOnLoad(p);
        }

        foreach (GameObject cam in cameras)
        {
            cam.transform.parent = null;
            DontDestroyOnLoad(cam);
        }

        foreach (GameObject gm in managers)
        {
            gm.transform.parent = null;
            DontDestroyOnLoad(gm);
        }

        foreach (GameObject btn in buttons)
        {
            DontDestroyOnLoad(btn);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTeleported) return;

        if (other.CompareTag("Player"))
        {
            hasTeleported = true;
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false; // 플레이어 컨트롤러 비활성화

                // 애니메이션 멈춤
                Animator playerAnimator = other.GetComponent<Animator>();
                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("walk", false);
                    playerAnimator.enabled = false; // 애니메이션 정지
                }

                // 오디오 정지
                AudioSource playerAudio = other.GetComponent<AudioSource>();
                if (playerAudio != null)
                {
                    playerAudio.Stop(); // 발소리 등 모든 오디오 정지
                }

                if (playerController != null)
                {
                    playerController.ResetMoveState();
                    playerController.enabled = false;
                    playerController.SetButtonsActive(false);

                }
            }

            StartCoroutine(FadeAndTeleport(other));
        }
    }


    IEnumerator FadeOutAtStart()
    {
        //float fadeCount = 1.0f;
        //while (fadeCount > 0)
        //{
        //    fadeCount -= 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    if (fadeImage != null)
        //        fadeImage.color = new Color(0, 0, 0, fadeCount);
        //}
        float fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= Time.deltaTime; // ← 프레임 시간에 따라 감소
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(fadeCount));
            yield return null; // 프레임마다 반복
        }
    }

    IEnumerator FadeAndTeleport(Collider2D other)
    {
        // 페이드 인
        //float fadeCount = 0;
        //while (fadeCount < 1.0f)
        //{
        //    fadeCount += 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    if (fadeImage != null)
        //        fadeImage.color = new Color(0, 0, 0, fadeCount);
        //}
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(fadeCount));
            yield return null;
        }

        // 씬 전환
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("bathroom wrong");
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        // GameManeger 접근 (null 체크)
        GameManeger gameManeger = GameManeger.Instance;
        if (gameManeger != null)
        {
            gameManeger.GameCount = 0;
        }
        else
        {
            Debug.LogWarning("FadeAndTeleport 중 GameManeger.Instance가 null입니다!");
        }

        // 잠깐 대기 후 페이드 아웃
        yield return new WaitForSeconds(0.05f);
        //fadeCount = 1.0f;
        //while (fadeCount > 0)
        //{
        //    fadeCount -= 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    if (fadeImage != null)
        //        fadeImage.color = new Color(0, 0, 0, fadeCount);
        //}
        fadeCount = 1.0f;
        while (fadeCount > 0)
        {
            fadeCount -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(fadeCount));
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject camera = GameObject.FindWithTag("MainCamera");

        if (player != null)
        {
            float playerX = player.transform.position.x;

            if (playerX < 15)
            {
                player.transform.position = new Vector3(playerX + 2.5f, player.transform.position.y, player.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(playerX - 2.5f, player.transform.position.y, player.transform.position.z);
            }

            // ✅ 씬 전환 후 애니메이션 다시 활성화
            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.enabled = true;  // 애니메이션 다시 활성화
                playerAnimator.Play("Idle");   // 애니메이션을 Idle 상태로 초기화
            }

            // ✅ 플레이어 컨트롤러 다시 활성화
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
                playerController.SetButtonsActive(true);
            }

            // ✅ 오디오 다시 활성화
            AudioSource playerAudio = player.GetComponent<AudioSource>();
            if (playerAudio != null)
            {
                playerAudio.Play(); // 필요한 경우 오디오 재생 (Idle 사운드 등)
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
