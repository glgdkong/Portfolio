using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 움직이는 함정 충돌 컴포넌트
public class MoveTrapCollision : CollisionComponent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 태그가 적이나 플레이어면
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            // 체력관리 컴포넌트 참조
            hp = collision.GetComponent<HealthControlComponent>();
            // 피격 처리
            hp.OnHit();
            StartCoroutine(DisablePhysicsCoroutine());
        }
    }
    // 물리충돌 비활성화 코루틴
    IEnumerator DisablePhysicsCoroutine()
    {
        // 함정과 플레이어와의 물리충돌 비활성화
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player") , LayerMask.NameToLayer("Trap"), true);
        yield return new WaitForSeconds(0.4f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Trap"), false);
    }
}
