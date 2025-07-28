using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorEvent : InteractiveEvent
{
    public bool isInside = false; // 플레이어가 내부에 있는지 여부

    public override void OnInteract()
    {
        GameManager.Instance.StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        // 화면 어둡게
        yield return GameManager.Instance.FadeScreen(1f, 0.5f);

        // 위치 이동
        if (isInside)
        {
            GameManager.Instance.Player.transform.position = new Vector3(3, 7, 0); // 메인 맵
            GameManager.Instance.MainCamera.transform.position = new Vector3(3, 7, -10); // 카메라 위치 조정
        }
        else
        {
            GameManager.Instance.Player.transform.position = new Vector3(8, 33, 0); // 미니게임 맵
            GameManager.Instance.MainCamera.transform.position = new Vector3(8, 33, -10); // 카메라 위치 조정
        }

        // 화면 밝게
        yield return GameManager.Instance.FadeScreen(0f, 0.5f);
    }
}
