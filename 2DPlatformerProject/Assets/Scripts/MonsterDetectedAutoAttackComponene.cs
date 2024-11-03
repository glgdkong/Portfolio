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
        // �÷��̾ ������ ���� ����
        while (!plaeyrHealthControl.IsDead)
        {
            yield return new WaitForSeconds(attackDelayTime);
            if(!isPaused)
            {// ���� ����
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // �±װ� �÷��̾���
                    if (collider.CompareTag("Player"))
                    {
                        // �÷��̾ ���ϴ� ���Ⱚ ����
                        Vector2 directions = (collider.transform.position - transform.position);
                        // ���ʹϾ� ����
                        Quaternion quaternion = transform.rotation;
                        // �÷��̾ ���ϴ� ���Ⱚ X �� ���� 0���� ũ�� ���ʹϾ� Y���� 0�� ��ȯ
                        // �ƴϸ� 180�� ��ȯ
                        quaternion.y = directions.x > 0f ? 0f : 180f;
                        // ���ʹϾ� ���� ȸ������ ����
                        transform.rotation = quaternion;
                        // �ִϸ����� ���� Ʈ���� �۵�
                        animator.SetTrigger("Attack");
                        yield return new WaitForSeconds(0.6f);
                    }

                }
            }

            // �÷��̾ ���� ���� ���� ��쿡�� ���
            if (!playerInRange)
            {
                yield return null; // ���� �����ӱ��� ���
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectedTransform.position, detectedRange);
    }

}
