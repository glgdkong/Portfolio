using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̴� ���� �浹 ������Ʈ
public class MoveTrapCollision : CollisionComponent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �±װ� ���̳� �÷��̾��
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            // ü�°��� ������Ʈ ����
            hp = collision.GetComponent<HealthControlComponent>();
            // �ǰ� ó��
            hp.OnHit();
            StartCoroutine(DisablePhysicsCoroutine());
        }
    }
    // �����浹 ��Ȱ��ȭ �ڷ�ƾ
    IEnumerator DisablePhysicsCoroutine()
    {
        // ������ �÷��̾���� �����浹 ��Ȱ��ȭ
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player") , LayerMask.NameToLayer("Trap"), true);
        yield return new WaitForSeconds(0.4f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Trap"), false);
    }
}
