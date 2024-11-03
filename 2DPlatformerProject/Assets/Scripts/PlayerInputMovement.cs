using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : DirectionHorizontalMovement
{
    [SerializeField] private bool isGrounded; // �ٴ� ���� ����
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    [SerializeField] private Transform groundCheck; // �ٴ� ���� ��ġ
    [SerializeField] private float groundCheckRadius;   // �ٴ� üũ ����
    [SerializeField] private LayerMask groundLayer; // �ٴ� ���̾�
    [SerializeField] private RectTransform heathCanvas;

    private bool jumpRequested;
    [SerializeField] private float jumpForce;   // ���� ��
    private bool isLoading = false;
    public bool IsLoading { get => isLoading; set => isLoading = value; }

    protected PlayerHealthControl plaeyrHealthControl;
    // �˹� ����
    private bool isKnockbacking;
    public bool IsKnockbacking { get => isKnockbacking; set => isKnockbacking = value; }
    // �˹� �ð� ����
    [SerializeField] protected float knockbackTime;



    protected override void Awake()
    {
        base.Awake();
        plaeyrHealthControl = GetComponent<PlayerHealthControl>();
    }
    protected override void Update()
    {
        // �ε� ���� �ų� �˹� ���¸� ����
        if (IsLoading || isKnockbacking || isPaused) return;

        Move();

        Jump();

    }

    protected override void Move()
    {
        // * Collider �浹�ݶ��̴����� = Physics2D.OverlapCircle(�浹üũ��ġ, �浹üũũ��, �浹���̾�);
        // ĳ���� �ٴ� ���� ���θ� üũ��
        IsGrounded = IsGroundedMethod();

        float moveInput = Input.GetAxisRaw("Horizontal");

        // ĳ���� ���� ��ȯ
        // - ĳ���Ͱ� �������� ���� �ִµ� Ű�� ������ �����ٸ� -> ����
        if ((IsRight && moveInput < 0f) || (!IsRight && moveInput > 0f))
        {
            Flip();
        }


        // �÷��̾� �̵� ó��
        rigidbody2D.velocity = new Vector2(moveInput * MoveSpeed, rigidbody2D.velocity.y);

        // * float ���밪 = Mathf.Abs(����/���);
        // * AnimatorSetFloat("�ִϸ������Ķ���͸�", ������);
        animator.SetFloat("Move", Mathf.Abs(moveInput));


        // �ٴ� ���� ���� �ִϸ��̼� �Ķ���� ����
        animator.SetBool("IsGround", IsGrounded);
        // ���� ���/�ϰ� ���� �ִϸ��̼� �Ķ���� ����
        animator.SetFloat("Vertical", rigidbody2D.velocity.y);

        // ���� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        {
            // ���� ���� ��û �÷��� ���� ����
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
        // ���� ��û ���¸�
        if (jumpRequested)
        {
            Vector2 currentVelocity = rigidbody2D.velocity;
            // ���� ��� ó����
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            // ���� �ִϸ��̼� ���
            animator.SetTrigger("Jump");
            // ���� ��û ����
            jumpRequested = false;
        }
    }

    // �÷��̾� �˹� ���� Ȱ��ȭ �ڷ�ƾ
    public IEnumerator PlayerKnockbackingCoroutine()
    {
        // �÷��̾� �˹� ���� Ȱ��ȭ
        IsKnockbacking = true;
        Debug.Log("�˹� ����");

        yield return new WaitForSeconds(knockbackTime);
        if (isPaused) { yield return new WaitForSeconds(knockbackTime); }
        // �÷��̾� �˹� ���� ��Ȱ��ȭ
        IsKnockbacking = false;
        Debug.Log("�˹� ��");
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
