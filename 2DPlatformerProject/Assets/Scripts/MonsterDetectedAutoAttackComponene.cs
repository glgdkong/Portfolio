using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MonsterDetectedAutoAttackComponene : MonsterAutoAttackComponent
{
    [SerializeField] protected float detectedRange;
    [SerializeField] protected Transform detectedTransform;
    bool playerInRange = false;
    protected override IEnumerator AutoFireCoroutine()
    {
        // 플레이어가 죽으면 공격 종료
        while (!plaeyrHealthControl.IsDead)
        {
            yield return new WaitForSeconds(attackDelayTime);
            if(!isPaused)
            {// 범위 감지
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // 태그가 플레이어라면
                    if (collider.CompareTag("Player"))
                    {
                        // 플레이어를 향하는 방향값 구함
                        Vector2 directions = (collider.transform.position - transform.position);
                        // 쿼터니언 생성
                        Quaternion quaternion = transform.rotation;
                        // 플레이어를 향하는 방향값 X 의 값이 0보다 크면 쿼터니언 Y값에 0을 반환
                        // 아니면 180을 반환
                        quaternion.y = directions.x > 0f ? 0f : 180f;
                        // 쿼터니언 값을 회전값에 적용
                        transform.rotation = quaternion;
                        // 애니메이터 공격 트리거 작동
                        animator.SetTrigger("Attack");
                        yield return new WaitForSeconds(0.6f);
                    }

                }
            }

            // 플레이어가 범위 내에 없을 경우에도 대기
            if (!playerInRange)
            {
                yield return null; // 다음 프레임까지 대기
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectedTransform.position, detectedRange);
    }

}
