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
                    // �±װ� �÷��̾���
                    if (collider.CompareTag("Player"))
                    {
                        // �÷��̾�� ������ ���⺤�� ����
                        Vector2 direction = (collider.transform.position - transform.position);
                        // ���⺤���� Y�� ����
                        direction.y = 0f;
                        // ���⺤�� ����ȭ
                        direction.Normalize();
                        // ���ʹϾ� ���� ȸ���� ����
                        Quaternion quaternion = transform.rotation;
                        // ���⺤�� x�� ���� 0���� ũ�� 0���� ��ȯ �ƴϸ� 180���� ��ȯ
                        // ��ȯ�� ���� ���ʹϾ� y���� ����
                        quaternion.y = direction.x > 0f ? 0f : 180f;
                        // ���� ȸ������ ���ʹϾ��� ����
                        transform.rotation = quaternion;
                        // �÷��̾� �������� ����
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
