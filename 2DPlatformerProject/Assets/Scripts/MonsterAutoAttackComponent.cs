using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ڵ� ���� ������Ʈ
public class MonsterAutoAttackComponent : AttackComopnent
{
    // ���� ������ �ð�
    [SerializeField] protected float attackDelayTime;
    // ������ �Ѿ� ������ �迭����
    [SerializeField] protected GameObject[] bulletPrefabs;
    // �Ѿ� ������ġ �迭 ����
    [SerializeField] protected Transform[] bulletSpawnPosition;
    // �߻��� �Ѿ� �ε���
    [SerializeField] protected int bulletIndex = 0;
    // �߻�� �Ѿ� ��ġ �ε���
    [SerializeField] protected int spawnIndex = 0;
    // ���� ������ ������Ʈ
    [SerializeField] protected GroundMonsterMovement groundMonsterMovement;

    [SerializeField] protected ParticleSystem particle;

    protected override void Attack()
    {
        // �ڵ� ���� �ڷ�ƾ ����
        StartCoroutine(AutoFireCoroutine());
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // ���� �޼ҵ� ����
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
        // �÷��̾ ������ ������ ���� �Ұ�
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
