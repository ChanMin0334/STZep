using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject highlightEffect; // 활성 효과 오브젝트(예: 빛나는 효과)

    void Start()
    {
        if (highlightEffect != null)
            highlightEffect.SetActive(false); // 시작 시 비활성화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (highlightEffect != null)
                highlightEffect.SetActive(true); // 플레이어가 닿으면 효과 활성화
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (highlightEffect != null)
                highlightEffect.SetActive(false); // 플레이어가 벗어나면 효과 비활성화
        }
    }
}
