using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthControlComponent : MonoBehaviour
{
    [Range(-1, 3)]
    [SerializeField] protected int healthPoint;
    protected Animator animator;
    [SerializeField] protected bool hitAble = true; // 피격 가능 처리
    protected SpriteRenderer spriteRenderer;
    public int HealthPoint { get => healthPoint;}

    protected virtual void Awake()
    {
        // 애니메이터 참조
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void OnHit(int damage = 1);
    protected abstract void Death();

    // 피격시간 지연 코루틴
    protected virtual IEnumerator HitColorCoroutine(int damage)
    {
        // 피격가능 비활성화
        hitAble = false;
        // 체력 포인트 감소
        healthPoint -=damage;
        // 스프라이트 색상 변경
        spriteRenderer.color = Color.red;
        // 0.1초 지연
        yield return new WaitForSeconds(0.1f);
        // 원래 색상 복원
        spriteRenderer.color = Color.white;
        if (healthPoint < 0) // 0 미만이면
        {
            // 사망처리
            Death();
            yield break; // 코루틴 종료
        }
        // 0.3초 후
        yield return new WaitForSeconds(0.3f);
        // 피격 가능 활성화
        hitAble = true;
    }


}
