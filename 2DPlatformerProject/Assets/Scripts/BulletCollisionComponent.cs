using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionComponent : CollisionComponent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딛힌 물체가 플레이어면
        if (collision.tag.Equals("Player"))
        {
            hp = collision.GetComponent<HealthControlComponent>();
            // 플레이어 피격 처리
            hp.OnHit();
            // 후에 불렛 삭제
            Destroy(gameObject);
            return;
        }
        if (collision.tag.Equals("Ground")) Destroy(gameObject); // 부딛힌 물체가 땅 태그면 게임오브젝트 삭제
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        // 월드 밖으로 나가면 게임 오브젝트 삭제
        if (collision.tag.Equals("World"))
        {
            Destroy(gameObject);
        }
    }
}
