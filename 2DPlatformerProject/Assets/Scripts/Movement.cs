using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̵� �߻� Ŭ����
public abstract class Movement : MonoBehaviour
{
    // �ִϸ����� ����
    protected Animator animator;
    // ������ �ٵ� ����
    protected new Rigidbody2D rigidbody2D;
    protected bool isPaused = false; // �Ͻ� ���� ����

    // �̵��ӵ�
    [SerializeField] protected float moveSpeed;

    // �̵� ����
    [SerializeField] protected Vector2 moveDirection;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Rigidbody2D Rigidbody2D { get => rigidbody2D; set => rigidbody2D = value; }
    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }

    private Vector2 pauseVectorSave;
    private float pauseGravityScaleSave;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // �̵� �߻� �޼ҵ�
    protected abstract void Move();

    protected virtual void OnEnable()
    {
        // ���� ������Ʈ�� ���� �Ͻ� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 �����
        PauseGameManager.onPauseDelegate += OnPause;
        // ���� ������Ʈ�� ���� �Ͻ� ���� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 �����
        PauseGameManager.onResumeDelegate += OnResume;


    }

    // OnDisable : ���ӿ�����Ʈ�� ��Ȱ��ȭ(SetActive(false)) ���� �� ȣ�� �Ǵ� �̺�Ʈ �޼ҵ�
    // * Destroy �ɶ��� ȣ���
    protected virtual void OnDisable()
    {
        // ���� ������Ʈ�� ���� �Ͻ� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 ����� ������
        PauseGameManager.onPauseDelegate -= OnPause;
        // ���� ������Ʈ�� ���� �Ͻ� ���� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 ����� ������
        PauseGameManager.onResumeDelegate -= OnResume;
    }

    protected virtual void OnPause()
    {
        isPaused = true; 
        animator.speed = 0;
        pauseGravityScaleSave = rigidbody2D.gravityScale;
        pauseVectorSave = rigidbody2D.velocity;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
    }

    protected virtual void OnResume()
    {
        isPaused = false;
        rigidbody2D.gravityScale = pauseGravityScaleSave;
        rigidbody2D.velocity = pauseVectorSave;
        animator.speed = 1;
    }
}
