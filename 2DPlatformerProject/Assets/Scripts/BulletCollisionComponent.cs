using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionComponent : CollisionComponent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ε��� ��ü�� �÷��̾��
        if (collision.tag.Equals("Player"))
        {
            hp = collision.GetComponent<HealthControlComponent>();
            // �÷��̾� �ǰ� ó��
            hp.OnHit();
            // �Ŀ� �ҷ� ����
            Destroy(gameObject);
            return;
        }
        if (collision.tag.Equals("Ground")) Destroy(gameObject); // �ε��� ��ü�� �� �±׸� ���ӿ�����Ʈ ����
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ������ ������ ���� ������Ʈ ����
        if (collision.tag.Equals("World"))
        {
            Destroy(gameObject);
        }
    }
}
