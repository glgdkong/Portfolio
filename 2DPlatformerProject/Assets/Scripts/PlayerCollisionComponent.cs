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
        // 플레이어가 살아있는 상태면
        if (!playerHealthControl.IsDead)
        {
            // 물리충돌한 게임오브젝트의 태그가 Enemy면
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                // EnemyHealthControl 컴포넌트를 참조
                EnemyHealthControl enemyHp = collision.gameObject.GetComponent<EnemyHealthControl>();
                // 밟은 게임오브젝트의 수직벡터의 Y값이 0.635 이상이고 밟을 수 있는 상대면
                if (collision.contacts[0].normal.y > 0.635 && enemyHp.IsHeadStep)
                {
                    // 플레이어의 윗 방향으로 튀어오름
                    rigidbody2D.velocity = Vector2.up * upSpeed;
                    // 후에 적 피격 처리
                    HealthControlComponent enemyHealth = collision.gameObject.GetComponent<EnemyHealthControl>();
                    enemyHealth.OnHit();
                    return;
                }
                else // 둘다 아니라면
                {
                    // 플레이어 피격 처리
                    playerHealthControl.OnHit();
                }
            }
        }
    }
}
