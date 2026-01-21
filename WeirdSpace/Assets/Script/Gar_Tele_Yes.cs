using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gar_Tele_Yes : MonoBehaviour
{
    int stageIndex;
    bool hasTeleported = false;
    public Image fadeImage;

    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        // 화면을 처음 어둡게 설정
        fadeImage.color = new Color(0, 0, 0, 1);

        // 게임 시작 시 페이드 아웃 실행
        StartCoroutine(FadeOutAtStart());


        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] camera = GameObject.FindGameObjectsWithTag("MainCamera");
        GameObject[] gamemaneger = GameObject.FindGameObjectsWithTag("GameManeger");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject p in player)
        {
            p.transform.parent = null;
            DontDestroyOnLoad(p);
        }
        foreach (GameObject cam in camera)
        {
            cam.transform.parent = null;
            DontDestroyOnLoad(cam);
        }

        foreach (GameObject gm in gamemaneger)
        {
            gm.transform.parent = null;
            DontDestroyOnLoad(gm);
        }

        foreach (GameObject btn in buttons)
        {
            DontDestroyOnLoad(btn);
        }
    }

    void KeepObjectPersistent(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                // UI인지 확인하고 적절한 부모 제거 방법 선택
                if (obj.GetComponent<RectTransform>() != null)
                {
                    obj.transform.SetParent(null, false); // UI 요소는 SetParent(null, false) 사용
                }
                else
                {
                    obj.transform.parent = null; // 일반 오브젝트는 parent = null 사용
                }

                DontDestroyOnLoad(obj); // 루트 오브젝트로 만든 후 DontDestroyOnLoad 적용
            }
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
                playerController.ResetMoveState();
                playerController.enabled = false;
                playerController.SetButtonsActive(false);
            }
            // ✅ 애니메이션 및 오디오 정지
            Animator playerAnimator = other.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("walk", false);
                playerAnimator.enabled = false;
            }

            AudioSource playerAudio = other.GetComponent<AudioSource>();
            if (playerAudio != null)
                playerAudio.Stop();

            StartCoroutine(FadeAndTeleport(other));

            GameManeger gameManeger = GameManeger.Instance;
            if (gameManeger == null)
            {
                Debug.LogError("GameManeger 컴포넌트를 찾을 수 없습니다!");
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void DestroyPersistentObjects()
    {
        // 삭제할 태그 목록
        string[] tagsToDestroy = { "Player", "MainCamera", "GameManeger", "Button" };

        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
    }

    IEnumerator FadeOutAtStart()
    {
        //float fadeCount = 1.0f;
        //while (fadeCount > 0)
        //{
        //    fadeCount -= 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    fadeImage.color = new Color(0, 0, 0, fadeCount);
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
        //float fadeCount = 0;
        //while (fadeCount < 1.0f)
        //{
        //    fadeCount += 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    fadeImage.color = new Color(0, 0, 0, fadeCount);
        //}
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(fadeCount));
            yield return null;
        }

        // 랜덤으로 스테이지 번호 결정
        int randomIndex = Random.Range(0, 6);
        if(randomIndex == 0)
        {
            stageIndex = 1;
        }
        else
        {
            int randomIndex1 = Random.Range(2, 37);
            stageIndex = Random.Range(2, 37);
        }
        Debug.Log("stageIndex : " + stageIndex);

        GameManeger gameManeger = GameManeger.Instance;
        // GameCount에 따른 씬 전환 로직
        if (gameManeger.GameCount >= 8) // 8번 연속 맞췄을 때
        {
            SceneManager.LoadScene("Clear"); // 적절한 씬 이름 입력
            DestroyPersistentObjects();
        }
        if (gameManeger.GameCount == 7) // 8번 연속 맞췄을 때
        {
            SceneManager.LoadScene("Garage Clear"); // 적절한 씬 이름 입력
            gameManeger.GameCount = 8;
        }
        else if (gameManeger.GameCount < 7) // 8회 이하일 때
        {
            SceneManager.LoadScene("Gar_" + stageIndex);
            gameManeger.GameCount = gameManeger.GameCount + 1;
            Debug.Log("Remaining attempts: " + gameManeger.GameCount);
            PlayerController playerController = other.GetComponent<PlayerController>();
            // Animator 가져와서 walk 비활성화
            playerController.animator.SetBool("walk", false);
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }

        yield return new WaitForSeconds(0.05f);
        //fadeCount = 1.0f;
        //while (fadeCount > 0)
        //{
        //    fadeCount -= 0.01f;
        //    yield return new WaitForSeconds(0.01f);
        //    fadeImage.color = new Color(0, 0, 0, fadeCount);
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

        if (player != null)
        {
            float playerX = player.transform.position.x;
            player.transform.position = new Vector3(playerX < 15 ? playerX + 2.5f : playerX - 2.5f, player.transform.position.y, player.transform.position.z);

            // ✅ 애니메이션 다시 활성화 및 Idle 초기화
            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.enabled = true;
                playerAnimator.Play("Idle");
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
                playerAudio.Play();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
