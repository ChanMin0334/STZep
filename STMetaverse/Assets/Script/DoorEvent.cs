using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorEvent : InteractiveEvent
{
    public bool isInside = false; // �÷��̾ ���ο� �ִ��� ����

    public override void OnInteract()
    {
        GameManager.Instance.StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        // ȭ�� ��Ӱ�
        yield return GameManager.Instance.FadeScreen(1f, 0.5f);

        // ��ġ �̵�
        if (isInside)
        {
            GameManager.Instance.Player.transform.position = new Vector3(3, 7, 0); // ���� ��
            GameManager.Instance.MainCamera.transform.position = new Vector3(3, 7, -10); // ī�޶� ��ġ ����
        }
        else
        {
            GameManager.Instance.Player.transform.position = new Vector3(8, 33, 0); // �̴ϰ��� ��
            GameManager.Instance.MainCamera.transform.position = new Vector3(8, 33, -10); // ī�޶� ��ġ ����
        }

        // ȭ�� ���
        yield return GameManager.Instance.FadeScreen(0f, 0.5f);
    }
}
