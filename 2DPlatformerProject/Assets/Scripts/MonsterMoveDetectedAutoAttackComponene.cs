using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveDetectedAutoAttackComponene : MonsterAutoAttackComponent
{
    // ���� ����
    [SerializeField] protected float detectedRange;
    // ���� Ʈ������
    [SerializeField] protected Transform detectedTransform;
    // ���� ��ī��Ʈ
    [SerializeField] protected int randomCount;

    protected override IEnumerator AutoFireCoroutine()
    {
        // �÷��̾ ������ ���� ����
        while (!plaeyrHealthControl.IsDead)
        {
            // ���� ������ ����
            yield return new WaitForSeconds(attackDelayTime);
            // ���� ��ī��Ʈ ��÷
            int random = Random.Range(0, randomCount);
            // ���� ����
            if (!isPaused)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // �±װ� �÷��̾�� 
                    if (collider.CompareTag("Player"))
                    {
                        // 0�̸� ���缭 ���ݽ���
                        switch (random)
                        {
                            case 0:
                                // ���� �̵��ӵ� ����
                                float moveSpeed = groundMonsterMovement.MoveSpeed;
                                // ���� �̵��ӵ� ����
                                groundMonsterMovement.MoveSpeed = 0;
                                // ���� Ʈ���� ����
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(0.6f);
                                // ���� �̵��ӵ� ����
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                break;
                            case 1:
                                // Ư�� ���� Ʈ���� ����
                                animator.SetTrigger("SpecialAttack");
                                yield return new WaitForSeconds(0.6f);
                                break;
                        }
                    }
                }

            }
        }
    }
    // ����� ���� �̺�Ʈ �޼ҵ�
    private void OnDrawGizmosSelected()
    {
        // ����� ���� ����
        Gizmos.color = Color.red;
        // ����� ����
        Gizmos.DrawWireSphere(detectedTransform.position, detectedRange);
    }
    public override void InstantiateBulletAnimatorEvent()
    {
        GameObject bullet;
        // �÷��̾ ������ ������ ���� �Ұ�
        if (!plaeyrHealthControl.IsDead)
        {
            bullet = Instantiate(bulletPrefabs[bulletIndex], bulletSpawnPosition[spawnIndex].position, bulletSpawnPosition[spawnIndex].rotation);
        }
    }
}
