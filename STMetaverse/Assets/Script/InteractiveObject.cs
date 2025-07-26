using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject highlightEffect; // Ȱ�� ȿ�� ������Ʈ(��: ������ ȿ��)

    void Start()
    {
        if (highlightEffect != null)
            highlightEffect.SetActive(false); // ���� �� ��Ȱ��ȭ
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (highlightEffect != null)
                highlightEffect.SetActive(true); // �÷��̾ ������ ȿ�� Ȱ��ȭ
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (highlightEffect != null)
                highlightEffect.SetActive(false); // �÷��̾ ����� ȿ�� ��Ȱ��ȭ
        }
    }
}
