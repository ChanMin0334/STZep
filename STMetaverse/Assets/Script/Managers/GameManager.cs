using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject MainCamera; // ���� ī�޶� ������Ʈ
    public GameObject Player;
    public Image fadeImage; // ��üȭ���� ���� UI Image(������, Canvas�� ��ġ)

    public Vector3 lastPlayerPosition; // �� �̵� ���� �÷��̾� ��ġ ����

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

    // �� �̸��� �޾Ƽ� �̵��ϴ� �Լ�
    public void GoToScene(string sceneName)
    {
        StartCoroutine(GoToSceneRoutine(sceneName));
    }

    private IEnumerator GoToSceneRoutine(string sceneName)
    {
        // ���� �÷��̾� ��ġ ����
        if (Player != null)
            lastPlayerPosition = Player.transform.position;

        // ���̵� �ƿ�
        yield return FadeScreen(1f, 0.5f);
        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
        // �� �ε� �� ���̵� ��
        yield return new WaitForSeconds(0.1f);
        yield return FadeScreen(0f, 0.5f);
    }

    // MainScene�� �ε���� �� �÷��̾� ��ġ ����
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

