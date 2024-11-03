using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveDetectedAutoAttackComponene : MonsterAutoAttackComponent
{
    // 감지 범위
    [SerializeField] protected float detectedRange;
    // 감지 트랜스폼
    [SerializeField] protected Transform detectedTransform;
    // 랜덤 인카운트
    [SerializeField] protected int randomCount;

    protected override IEnumerator AutoFireCoroutine()
    {
        // 플레이어가 죽으면 공격 종료
        while (!plaeyrHealthControl.IsDead)
        {
            // 공격 딜레이 설정
            yield return new WaitForSeconds(attackDelayTime);
            // 랜덤 인카운트 추첨
            int random = Random.Range(0, randomCount);
            // 범위 감지
            if (!isPaused)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // 태그가 플레이어면 
                    if (collider.CompareTag("Player"))
                    {
                        // 0이면 멈춰서 공격실행
                        switch (random)
                        {
                            case 0:
                                // 몬스터 이동속도 저장
                                float moveSpeed = groundMonsterMovement.MoveSpeed;
                                // 몬스터 이동속도 제거
                                groundMonsterMovement.MoveSpeed = 0;
                                // 공격 트리거 설정
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(0.6f);
                                // 몬스터 이동속도 복원
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                break;
                            case 1:
                                // 특수 공격 트리거 설정
                                animator.SetTrigger("SpecialAttack");
                                yield return new WaitForSeconds(0.6f);
                                break;
                        }
                    }
                }

            }
        }
    }
    // 기즈모 생성 이벤트 메소드
    private void OnDrawGizmosSelected()
    {
        // 기즈모 색상 설정
        Gizmos.color = Color.red;
        // 기즈모 생성
        Gizmos.DrawWireSphere(detectedTransform.position, detectedRange);
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
}
