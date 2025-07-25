using UnityEngine;

public enum PlayerState
{
    idle,
    move
}

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public Animator animator;    // 애니메이터 컴포넌트 참조
    private bool facingRight = false; // 현재 바라보는 방향 (왼쪽이 기본)
    private PlayerState _playerState = PlayerState.idle;

    // Update is called once per frame
    void Update()
    {
        // 입력값 받아오기
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D 또는 좌/우 화살표
        float moveY = Input.GetAxisRaw("Vertical");   // W, S 또는 상/하 화살표

        // 이동 벡터 계산 (2D에서는 X, Y만 사용)
        Vector3 move = new Vector3(moveX, moveY, 0).normalized;

        // PlayerState 판별
        if (move.sqrMagnitude > 0)
        {
            _playerState = PlayerState.move;
            animator.SetBool("Run", true);
        }
        else
        {
            _playerState = PlayerState.idle;
            animator.SetBool("Run", false);
        }
        // 이동 적용 (move 상태일 때만 이동)
        if (_playerState == PlayerState.move)
        {
            transform.position += move * moveSpeed * Time.deltaTime;
        }

        // 좌우 방향에 따라 스프라이트 뒤집기
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    // 스프라이트 뒤집기 함수
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
