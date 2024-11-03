using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UsingItems : MonoBehaviour
{
    // ������ ��� ���� �ʱⰪ Ʈ��
    protected bool isItemUseAble = true;
    // �ִϸ����� ����
    protected Animator animator;
    [SerializeField] protected float detectedRange;
    [SerializeField] protected LayerMask layerMask;
    protected Rigidbody2D rigid;
    // ���� ���� ������
    [SerializeField] protected GameObject soundPrefab;

    protected virtual void Awake()
    {
        // �ִϸ����� ������Ʈ ������
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        // ���� ����
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, detectedRange, layerMask);
        foreach (var col in collider)
        {
            // ���� �ȿ� �ִ� ��ü�� �÷��̾�� �Ʒ�����Ű�� ������
            if(col.CompareTag("Player") && Input.GetKeyDown(KeyCode.DownArrow))
            {
                // ������ ��� �޼ҵ� ����
                ItemUse(col);
            }
            return;
        }
    }
    protected virtual void OnEnable()
    {
        UpVelocity();
    }

    public abstract void ItemUse(Collider2D collider);
    protected virtual void UpVelocity()
    {
        rigid.velocity = Vector3.up * 3.5f;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectedRange);
    }
}
