using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : CollisionComponent
{
    // ��ü�� ���� ���¸�
    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���� ��ü�� �÷��̾� �Ǵ� ���̸�
        if (collision.tag.Equals("Player") || collision.tag.Equals("Enemy"))
        {
            // �ǰ� ó��
            hp =  collision.GetComponent<HealthControlComponent>();
            hp.OnHit();
        }
    }

}
