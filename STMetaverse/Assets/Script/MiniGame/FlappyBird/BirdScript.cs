using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float jumpForce = 2.5f; // Á¡ÇÁ Èû
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (FlappyBirdManager.Instance.isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    void Jump()
    {
        if(rb == null)
            return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        FlappyBirdManager.Instance.StopGame();
    }
}

