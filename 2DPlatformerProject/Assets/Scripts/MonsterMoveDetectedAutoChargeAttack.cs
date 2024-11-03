using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveDetectedAutoChargeAttack : MonsterAttacktoPlayerByBody
{
    // ���� Ʈ������
    [SerializeField] protected Transform detectedTransform;
    // ���� ��ī��Ʈ
    [SerializeField] protected int randomCount;
    // �̵� ������Ʈ
    [SerializeField] protected GroundMonsterMovement groundMonsterMovement;
    // ���� ����
    protected Vector2 direction;
    // ���� ���� ����
    [Range(0, 1)] protected int isChargeAttacking;
    // �˹� �ð� ����
    [SerializeField] protected float knockbackTime;


    protected override IEnumerator AttackDelay()
    {
        // �÷��̾ ������ ���� ����
        while (!plaeyrHealthControl.IsDead)
        {
            // ���� ������ ����
            yield return new WaitForSeconds(attackDelayTime);
            // ���� ��ī��Ʈ ��÷
            int random = Random.Range(0, randomCount);
            if (!isPaused)
            {// ���� ����
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(detectedTransform.position, detectedRange, layerMask);
                foreach (var collider in collider2Ds)
                {
                    // �±װ� �÷��̾�� 
                    if (collider.CompareTag("Player"))
                    {
                        // ���� ���� ȸ�� �޼ҵ� ����
                        ChargeAttackRotation(collider.transform);
                        // 0�̸� ���缭 ���ݽ���
                        switch (random)
                        {
                            case 0:
                                // ���� �̵��ӵ� ����
                                float moveSpeed = groundMonsterMovement.MoveSpeed;
                                // ���� �̵��ӵ� ����
                                groundMonsterMovement.MoveSpeed = 0;
                                // �������� �Ŀ� ����
                                float charge = chargePower;
                                // �������� �Ŀ� ����
                                chargePower += 1.2f;
                                // ���� Ʈ���� ����
                                animator.SetTrigger("Attack");
                                yield return new WaitForSeconds(0.6f);
                                // ���� �̵��ӵ� ����
                                groundMonsterMovement.MoveSpeed = moveSpeed;
                                // �������� �Ŀ� ����
                                chargePower = charge;
                                break;
                            case 1:
                                // Ư�� ���� Ʈ���� ����
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

            // �浹�� ��ü�� ��ġ
            Vector2 otherPosition = collision.transform.position;

            // ���� ���� ��� (�浹�� ��ü���� �� ��ü���� ����)
            Vector3 vector = (collision.transform.position - transform.position).normalized;


            // �ǰ� ���¿��� �˹��� ���
            Rigidbody2D r2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (r2d != null && vector.y <= 0.635)
            {
                StopCoroutine(collision.gameObject.GetComponent<PlayerInputMovement>().PlayerKnockbackingCoroutine());
                StartCoroutine(collision.gameObject.GetComponent<PlayerInputMovement>().PlayerKnockbackingCoroutine());
                // �˹� ���� ��� (���� -> �÷��̾�)
                Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                knockbackDirection.y = 0; // ���� ���� ����
                // �ڷ� �˹� ó��
                r2d.AddForce(knockbackDirection * (chargePower * 1.5f), ForceMode2D.Impulse);
                r2d.AddForce(Vector2.up * chargePower / 2, ForceMode2D.Impulse); // ���� �� �߰�
            }
        }
    }
    // �������� ȸ�� �޼ҵ�
    private void ChargeAttackRotation(Transform playerPos)
    {
        // �÷��̾�� ������ ���⺤�� ����
        direction = (playerPos.transform.position - transform.position);
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
    }
    // ���� ���� �ִϸ����� �̺�Ʈ �޼ҵ�
    private void ChargeAttackAnimatorEvent()
    {
        // �÷��̾� �������� ����
        body.AddForce(direction * chargePower, ForceMode2D.Impulse);
    }

    private void IsChargeAttackingAniamtorEvent(int charge)
    {
        isChargeAttacking = charge;
    }
}
