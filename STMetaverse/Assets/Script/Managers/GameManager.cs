using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject MainCamera; // 메인 카메라 오브젝트
    public GameObject Player;
    public Image fadeImage; // 전체화면을 덮는 UI Image(검은색, Canvas에 배치)

    public Vector3 lastPlayerPosition; // 씬 이동 직전 플레이어 위치 저장

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeScreen(float targetAlpha, float duration)
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = targetAlpha;
        fadeImage.color = color;
    }

    // 씬 이름을 받아서 이동하는 함수
    public void GoToScene(string sceneName)
    {
        StartCoroutine(GoToSceneRoutine(sceneName));
    }

    private IEnumerator GoToSceneRoutine(string sceneName)
    {
        // 현재 플레이어 위치 저장
        if (Player != null)
            lastPlayerPosition = Player.transform.position;

        // 페이드 아웃
        yield return FadeScreen(1f, 0.5f);
        // 씬 전환
        SceneManager.LoadScene(sceneName);
        // 씬 로딩 후 페이드 인
        yield return new WaitForSeconds(0.1f);
        yield return FadeScreen(0f, 0.5f);
    }

    // MainScene이 로드됐을 때 플레이어 위치 복원
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" && Player != null)
        {
            Player.transform.position = lastPlayerPosition;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

