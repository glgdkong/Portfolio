using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCollisionComponent : CollisionComponent
{
    protected new Rigidbody2D rigidbody2D;
    protected const float upSpeed = 9.5f;
    protected bool isItemUsable = false;
    protected PlayerHealthControl playerHealthControl;

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerHealthControl = GetComponent<PlayerHealthControl>();
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾ ����ִ� ���¸�
        if (!playerHealthControl.IsDead)
        {
            // �����浹�� ���ӿ�����Ʈ�� �±װ� Enemy��
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                // EnemyHealthControl ������Ʈ�� ����
                EnemyHealthControl enemyHp = collision.gameObject.GetComponent<EnemyHealthControl>();
                // ���� ���ӿ�����Ʈ�� ���������� Y���� 0.635 �̻��̰� ���� �� �ִ� ����
                if (collision.contacts[0].normal.y > 0.635 && enemyHp.IsHeadStep)
                {
                    // �÷��̾��� �� �������� Ƣ�����
                    rigidbody2D.velocity = Vector2.up * upSpeed;
                    // �Ŀ� �� �ǰ� ó��
                    HealthControlComponent enemyHealth = collision.gameObject.GetComponent<EnemyHealthControl>();
                    enemyHealth.OnHit();
                    return;
                }
                else // �Ѵ� �ƴ϶��
                {
                    // �÷��̾� �ǰ� ó��
                    playerHealthControl.OnHit();
                }
            }
        }
    }
}
