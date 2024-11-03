using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComopnent : MonoBehaviour
{
    protected bool isPaused = false; // 일시 정지 여부
    // 오디오 소스 참조
    // 애니메이터 참조 
    protected Animator animator;
    [SerializeField] protected LayerMask layerMask;
    protected PlayerHealthControl plaeyrHealthControl;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        plaeyrHealthControl = FindObjectOfType<PlayerHealthControl>();
    }
    protected abstract void Attack();
    protected virtual void Start()
    {
        Attack();
    }
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
    }

    protected virtual void OnResume()
    {
        isPaused = false;
        animator.speed = 1;
    }


}
