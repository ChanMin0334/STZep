using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MiniGameType
{
    None = 0,
    FlappyBird = 1,
    Stack = 2,
    Shooting = 3
}
public class MiniGameEvent : InteractiveEvent
{
    [SerializeField]
    MiniGameType miniGameType = MiniGameType.None;
    public override void OnInteract()
    {
        switch (miniGameType)
        {
            case MiniGameType.FlappyBird:
                GameManager.Instance.GoToScene("FlappyBird");
                break;
            case MiniGameType.Stack:
                //GameManager.Instance.GoToScene("Stack");
                break;
            case MiniGameType.Shooting:
                //GameManager.Instance.GoToScene("Shooting");
                break;
            default:
                break;
        }
    }
}
