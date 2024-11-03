using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 자동 공격 컴포넌트
public class MonsterAutoAttackComponent : AttackComopnent
{
    // 공격 딜레이 시간
    [SerializeField] protected float attackDelayTime;
    // 공격할 총알 프리팹 배열참조
    [SerializeField] protected GameObject[] bulletPrefabs;
    // 총알 생성위치 배열 참조
    [SerializeField] protected Transform[] bulletSpawnPosition;
    // 발사할 총알 인덱스
    [SerializeField] protected int bulletIndex = 0;
    // 발사랑 총알 위치 인덱스
    [SerializeField] protected int spawnIndex = 0;
    // 몬스터 움직임 컴포넌트
    [SerializeField] protected GroundMonsterMovement groundMonsterMovement;

    [SerializeField] protected ParticleSystem particle;

    protected override void Attack()
    {
        // 자동 공격 코루틴 실행
        StartCoroutine(AutoFireCoroutine());
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // 공격 메소드 실행
        Attack();
    }
    protected virtual IEnumerator AutoFireCoroutine()
    {
        while (!plaeyrHealthControl.IsDead)
        {
            yield return new WaitForSeconds(attackDelayTime);
            if (!isPaused)
            {
                float moveSpeed = groundMonsterMovement.MoveSpeed;
                groundMonsterMovement.MoveSpeed = 0;
                animator.SetTrigger("Attack");

                yield return new WaitForSeconds(0.6f);
                groundMonsterMovement.MoveSpeed = moveSpeed;
            }
        }
    }

    // 
    public virtual void InstantiateBulletAnimatorEvent()
    {
        // 플레이어가 죽으면 프리팹 생성 불가
        if (!plaeyrHealthControl.IsDead)
             Instantiate(bulletPrefabs[bulletIndex], bulletSpawnPosition[spawnIndex].position, bulletSpawnPosition[spawnIndex].rotation); 
    }

    protected void OnParticleAnimatorEvent()
    {
        particle.Play();
    }
    protected void OffParticleAnimatorEvent()
    {
        particle.Stop();
    }
}
