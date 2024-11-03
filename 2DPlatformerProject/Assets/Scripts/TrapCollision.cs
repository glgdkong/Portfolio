using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : CollisionComponent
{
    // 물체와 닿은 상태면
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 닿은 물체가 플레이어 또는 적이면
        if (collision.tag.Equals("Player") || collision.tag.Equals("Enemy"))
        {
            // 피격 처리
            hp =  collision.GetComponent<HealthControlComponent>();
            hp.OnHit();
        }
    }

}
