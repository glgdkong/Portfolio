using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이동 추상 클래스
public abstract class Movement : MonoBehaviour
{
    // 애니메이터 참조
    protected Animator animator;
    // 리지드 바디 참조
    protected new Rigidbody2D rigidbody2D;
    protected bool isPaused = false; // 일시 정지 여부

    // 이동속도
    [SerializeField] protected float moveSpeed;

    // 이동 방향
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
    // 이동 추상 메소드
    protected abstract void Move();

    protected virtual void OnEnable()
    {
        // 현재 컴포넌트는 게임 일시 정지 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록함
        PauseGameManager.onPauseDelegate += OnPause;
        // 현재 컴포넌트는 게임 일시 정지 해제 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록함
        PauseGameManager.onResumeDelegate += OnResume;


    }

    // OnDisable : 게임오브젝트가 비활성화(SetActive(false)) 됐을 때 호출 되는 이벤트 메소드
    // * Destroy 될때도 호출됨
    protected virtual void OnDisable()
    {
        // 현재 컴포넌트는 게임 일시 정지 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록을 해제함
        PauseGameManager.onPauseDelegate -= OnPause;
        // 현재 컴포넌트는 게임 일시 정지 해제 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록을 해제함
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
