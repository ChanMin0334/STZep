using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public string interactKey = "f"; // ��ȣ�ۿ� Ű
    public GameObject balloonText; // ��ǳ�� �ؽ�Ʈ ������Ʈ
    public float fadeDuration = 0.3f; // ���̵� ��/�ƿ� �ð�(��)

    private InteractiveEvent interactiveEvent; // ��ȣ�ۿ� �̺�Ʈ ��ũ��Ʈ ����
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ִ��� ����
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
                StartFade(1f); // ���� �������
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
                StartFade(0f); // ���� ��ο�����
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
