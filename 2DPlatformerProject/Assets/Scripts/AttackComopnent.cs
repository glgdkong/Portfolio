using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComopnent : MonoBehaviour
{
    protected bool isPaused = false; // �Ͻ� ���� ����
    // ����� �ҽ� ����
    // �ִϸ����� ���� 
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
    }

    protected virtual void OnResume()
    {
        isPaused = false;
        animator.speed = 1;
    }


}
