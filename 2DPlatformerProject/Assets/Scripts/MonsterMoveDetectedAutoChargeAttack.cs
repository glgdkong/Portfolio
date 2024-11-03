using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveDetectedAutoChargeAttack : MonsterAttacktoPlayerByBody
{
    // 감지 트랜스폼
    [SerializeField] protected Transform detectedTransform;
    // 랜덤 인카운트
    [SerializeField] protected int randomCount;
    // 이동 컴포넌트
    [SerializeField] protected GroundMonsterMovement groundMonsterMovement;
    // 방향 벡터
    protected Vector2 direction;
    // 돌진 공격 여부
    [Range(0, 1)] protected int isChargeAttacking;
    // 넉백 시간 설정
    [SerializeField] protected float knockbackTime;


    protected override IEnumerator AttackDelay()
    {
        // 플레이어가 죽으면 공격 종료
        while (!plaeyrHealthControl.IsDead)
        {
            // 공격 딜레이 설정
            yield return new WaitForSeconds(attackDelayTime);
            // 랜덤 인카운트 추첨
            int random = Random.Range(0, randomCount);
            if (!isPaused)
            {// 범위 감지
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // 태그가 플레이어면 
                    if (collider.CompareTag("Player"))
                    {
                        // 차지 공격 회전 메소드 실행
                        ChargeAttackRotation(collider.transform);
                        // 0이면 멈춰서 공격실행
                        switch (random)
                        {
                            case 0:
                                // 몬스터 이동속도 저장
                                float moveSpeed = groundMonsterMovement.MoveSpeed;
                                // 몬스터 이동속도 제거
                                groundMonsterMovement.MoveSpeed = 0;
                                // 차지공격 파워 저장
                                float charge = chargePower;
                                // 차지공격 파워 증가
                                chargePower += 1.2f;
                                // 공격 트리거 설정
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(0.6f);
                                // 몬스터 이동속도 복원
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                // 차지공격 파워 복원
                                chargePower = charge;
                                break;
                            case 1:
                                // 특수 공격 트리거 설정
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(1f);
                                break;

                        }
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isChargeAttacking == 1)
        {

            // 충돌한 물체의 위치
            Vector2 otherPosition = collision.transform.position;

            // 범선 벡터 계산 (충돌한 물체에서 내 물체로의 방향)
            Vector3 vector = (collision.transform.position - transform.position).normalized;


            // 피격 상태에서 넉백을 허용
            Rigidbody2D r2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (r2d != null && vector.y <= 0.635)
            {
                StopCoroutine(collision.gameObject.GetComponent<PlayerInputMovement>().PlayerKnockbackingCoroutine());
                StartCoroutine(collision.gameObject.GetComponent<PlayerInputMovement>().PlayerKnockbackingCoroutine());
                // 넉백 방향 계산 (몬스터 -> 플레이어)
                Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                knockbackDirection.y = 0; // 수직 방향 제거
                // 뒤로 넉백 처리
                r2d.AddForce(knockbackDirection * (chargePower * 1.5f), ForceMode2D.Impulse);
                r2d.AddForce(Vector2.up * chargePower / 2, ForceMode2D.Impulse); // 위쪽 힘 추가
            }
        }
    }
    // 차지공격 회전 메소드
    private void ChargeAttackRotation(Transform playerPos)
    {
        // 플레이어와 몬스터의 방향벡터 생성
        direction = (playerPos.transform.position - transform.position);
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
    }
    // 차지 공격 애니메이터 이벤트 메소드
    private void ChargeAttackAnimatorEvent()
    {
        // 플레이어 방향으로 돌진
        body.AddForce(direction * chargePower, ForceMode2D.Impulse);
    }

    private void IsChargeAttackingAniamtorEvent(int charge)
    {
        isChargeAttacking = charge;
    }
}
