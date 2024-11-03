using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : DirectionHorizontalMovement
{
    [SerializeField] private bool isGrounded; // 바닥 착지 여부
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    [SerializeField] private Transform groundCheck; // 바닥 착지 위치
    [SerializeField] private float groundCheckRadius;   // 바닥 체크 범위
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    [SerializeField] private RectTransform heathCanvas;

    private bool jumpRequested;
    [SerializeField] private float jumpForce;   // 점프 힘
    private bool isLoading = false;
    public bool IsLoading { get => isLoading; set => isLoading = value; }

    protected PlayerHealthControl plaeyrHealthControl;
    // 넉백 여부
    private bool isKnockbacking;
    public bool IsKnockbacking { get => isKnockbacking; set => isKnockbacking = value; }
    // 넉백 시간 설정
    [SerializeField] protected float knockbackTime;



    protected override void Awake()
    {
        base.Awake();
        plaeyrHealthControl = GetComponent<PlayerHealthControl>();
    }
    protected override void Update()
    {
        // 로딩 중이 거나 넉백 상태면 중지
        if (IsLoading || isKnockbacking || isPaused) return;

        Move();

        Jump();

    }

    protected override void Move()
    {
        // * Collider 충돌콜라이더참조 = Physics2D.OverlapCircle(충돌체크위치, 충돌체크크기, 충돌레이어);
        // 캐릭터 바닥 착지 여부를 체크함
        IsGrounded = IsGroundedMethod();

        float moveInput = Input.GetAxisRaw("Horizontal");

        // 캐릭터 방향 전환
        // - 캐릭터가 오른쪽을 보고 있는데 키는 왼쪽을 눌렀다면 -> 반전
        if ((IsRight && moveInput < 0f) || (!IsRight && moveInput > 0f))
        {
            Flip();
        }


        // 플레이어 이동 처리
        rigidbody2D.velocity = new Vector2(moveInput * MoveSpeed, rigidbody2D.velocity.y);

        // * float 절대값 = Mathf.Abs(음수/양수);
        // * AnimatorSetFloat("애니메이터파라미터명", 설정값);
        animator.SetFloat("Move", Mathf.Abs(moveInput));


        // 바닥 착지 관련 애니메이션 파라미터 설정
        animator.SetBool("IsGround", IsGrounded);
        // 수직 상승/하강 관련 애니메이션 파라미터 설정
        animator.SetFloat("Vertical", rigidbody2D.velocity.y);

        // 점프 키 입력 처리
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        {
            // 점프 실행 요청 플래그 변수 설정
            jumpRequested = true;
        }
        if (plaeyrHealthControl.IsDead) rigidbody2D.velocity = Vector3.zero;
    }
    protected override void Flip()
    {
        base.Flip();
        flipQuaternion = Quaternion.AngleAxis(0f, Vector3.up);
        heathCanvas.rotation = flipQuaternion;
    }
    protected void Jump()
    {
        // 점프 요청 상태면
        if (jumpRequested)
        {
            Vector2 currentVelocity = rigidbody2D.velocity;
            // 수직 상승 처리함
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            // 점프 애니메이션 재생
            animator.SetTrigger("Jump");
            // 점프 요청 리셋
            jumpRequested = false;
        }
    }

    // 플레이어 넉백 여부 활성화 코루틴
    public IEnumerator PlayerKnockbackingCoroutine()
    {
        // 플레이어 넉백 여부 활성화
        IsKnockbacking = true;
        Debug.Log("넉백 시작");

        yield return new WaitForSeconds(knockbackTime);
        if (isPaused) { yield return new WaitForSeconds(knockbackTime); }
        // 플레이어 넉백 여부 비활성화
        IsKnockbacking = false;
        Debug.Log("넉백 끝");
        yield break;
    }

    public bool IsGroundedMethod()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    protected override void OnPause()
    {
        base.OnPause();

    }
    protected override void OnResume()
    {
        base.OnResume();
    }
}
