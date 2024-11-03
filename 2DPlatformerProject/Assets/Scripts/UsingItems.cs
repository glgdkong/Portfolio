using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UsingItems : MonoBehaviour
{
    // 아이템 사용 여부 초기값 트루
    protected bool isItemUseAble = true;
    // 애니메이터 참조
    protected Animator animator;
    [SerializeField] protected float detectedRange;
    [SerializeField] protected LayerMask layerMask;
    protected Rigidbody2D rigid;
    // 사운드 생성 프리펩
    [SerializeField] protected GameObject soundPrefab;

    protected virtual void Awake()
    {
        // 애니메이터 컴포넌트 가져옴
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        // 범위 감지
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, detectedRange, layerMask);
        foreach (var col in collider)
        {
            // 범위 안에 있는 객체가 플레이어고 아래방향키를 누르면
            if(col.CompareTag("Player") && Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 아이템 사용 메소드 실행
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
