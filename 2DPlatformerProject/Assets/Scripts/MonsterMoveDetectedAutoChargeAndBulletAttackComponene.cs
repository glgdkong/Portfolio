using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveDetectedAutoChargeAndBulletAttackComponene : MonsterAutoAttackComponent
{
    // 감지 범위
    [SerializeField] protected float detectedRange;
    // 감지 트랜스폼
    [SerializeField] protected Transform detectedTransform;
    // 랜덤 인카운트
    [SerializeField] protected int randomCount;
    // 돌진 공격 힘
    [SerializeField] protected float chargePower;
    // 리지드바디2D 컴포넌트
    [SerializeField] protected Rigidbody2D r2D;
    // 방향 벡터
    protected Vector2 direction;
    // 돌진 공격 여부
    [Range(0, 1)] protected int isChargeAttacking;
    // 넉백 시간 설정
    [SerializeField] protected float knockbackTime;


    protected override IEnumerator AutoFireCoroutine()
    {
        // 플레이어가 죽으면 공격 종료
        while (!plaeyrHealthControl.IsDead)
        {
            // 공격 딜레이 설정
            yield return new WaitForSeconds(attackDelayTime);
            // 랜덤 인카운트 추첨
            int random = Random.Range(0, randomCount);
            if(!isPaused)
            {// 범위 감지
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // 태그가 플레이어면 
                    if (collider.CompareTag("Player"))
                    {
                        float moveSpeed;
                        // 0이면 멈춰서 공격실행
                        switch (random)
                        {
                            case 0:
                                moveSpeed = groundMonsterMovement.MoveSpeed;
                                groundMonsterMovement.MoveSpeed = 0;
                                // 공격 트리거 설정
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(0.6f);
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                break;
                            case 1:
                                moveSpeed = groundMonsterMovement.MoveSpeed;
                                groundMonsterMovement.MoveSpeed = 0;
                                // 공격 트리거 설정
                                ChargeAttackRotation(collider.transform);
                                animator.SetTrigger("SpecialAttack");
                                yield return new WaitForSeconds(1.5f);
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                break;
                        }

                    }
                }
            }
        }
    }
    public override void InstantiateBulletAnimatorEvent()
    {
        GameObject bullet;
        // 플레이어가 죽으면 프리팹 생성 불가
        if (!plaeyrHealthControl.IsDead)
        {
            bullet = Instantiate(bulletPrefabs[bulletIndex], bulletSpawnPosition[spawnIndex].position, bulletSpawnPosition[spawnIndex].rotation);
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
        r2D.velocity = direction * chargePower;
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

    private void IsChargeAttackingAniamtorEvent(int charge)
    {
        isChargeAttacking = charge;
    }

    // 기즈모 생성 이벤트 메소드
    private void OnDrawGizmosSelected()
    {
        // 기즈모 색상 설정
        Gizmos.color = Color.red;
        // 기즈모 생성
        Gizmos.DrawWireSphere(detectedTransform.position, detectedRange);
    }
}
