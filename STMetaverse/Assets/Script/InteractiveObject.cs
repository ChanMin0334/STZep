using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public string interactKey = "f"; // 상호작용 키
    public GameObject balloonText; // 말풍선 텍스트 오브젝트
    public float fadeDuration = 0.3f; // 페이드 인/아웃 시간(초)

    private InteractiveEvent interactiveEvent; // 상호작용 이벤트 스크립트 참조
    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 여부
    private CanvasGroup balloonCanvasGroup;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (balloonText != null)
        {
            balloonCanvasGroup = balloonText.GetComponent<CanvasGroup>();
            if (balloonCanvasGroup == null)
                balloonCanvasGroup = balloonText.AddComponent<CanvasGroup>();

            balloonCanvasGroup.alpha = 0f;
            balloonText.SetActive(false);

            interactiveEvent = gameObject.GetComponent<InteractiveEvent>();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey.ToLower()))
        {
            OnInteract();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (balloonText != null)
            {
                balloonText.SetActive(true);
                StartFade(1f); // 점점 밝아지게
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (balloonText != null)
            {
                StartFade(0f); // 점점 어두워지게
            }
        }
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeBalloon(targetAlpha));
    }

    private IEnumerator FadeBalloon(float targetAlpha)
    {
        if (balloonCanvasGroup == null)
            yield break;

        float startAlpha = balloonCanvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            balloonCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }
        balloonCanvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0f)
            balloonText.SetActive(false);
    }

    private void OnInteract()
    {
        interactiveEvent.OnInteract();
    }
}
