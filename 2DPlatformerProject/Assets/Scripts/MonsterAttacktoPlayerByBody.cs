using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttacktoPlayerByBody : AttackComopnent
{
    [SerializeField] protected float detectedRange;
    [SerializeField] protected float attackDelayTime;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected float chargePower;
    [SerializeField] protected Vector2 randomAttackSpeedRange;

    protected override void Attack()
    {
        chargePower = Random.Range(randomAttackSpeedRange.x, randomAttackSpeedRange.y);
        StartCoroutine(AttackDelay());
    }
    protected virtual IEnumerator AttackDelay()
    {
        while (!plaeyrHealthControl.IsDead)
        {
            yield return new WaitForSeconds(attackDelayTime); 
            if (!isPaused)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectedRange, layerMask);
                foreach (var collider in colliders)
                {
                    // 태그가 플레이어라면
                    if (collider.CompareTag("Player"))
                    {
                        // 플레이어와 몬스터의 방향벡터 생성
                        Vector2 direction = (collider.transform.position - transform.position);
                        // 방향벡터의 Y값 제거
                        direction.y = 0f;
                        // 방향벡터 정규화
                        direction.Normalize();
                        // 쿼터니언에 현재 회전값 저장
                        Quaternion quaternion = transform.rotation;
                        // 방향벡터 x의 값이 0보다 크면 0값을 반환 아니면 180값을 반환
                        // 반환한 값을 쿼터니언 y값에 저장
                        quaternion.y = direction.x > 0f ? 0f : 180f;
                        // 현재 회전값에 쿼터니언을 적용
                        transform.rotation = quaternion;
                        // 플레이어 방향으로 돌진
                        body.velocity = direction * chargePower;
                        animator.SetTrigger("Attack");
                        chargePower = Random.Range(randomAttackSpeedRange.x, randomAttackSpeedRange.y);
                        yield return null;
                    }
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);
    }

}
