using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : UsingItems
{
    [SerializeField] private GameObject bombEffect;
    [SerializeField] private Transform explosionPoint;
    [SerializeField] private int explosionDamage;
    private void OnDestroy()
    {
        BomeDamager();
        GameObject bs = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        Destroy(bs, 1f);
        GameObject effect = Instantiate(bombEffect, explosionPoint.position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
    public override void ItemUse(Collider2D collider2D)
    {
        Destroy(gameObject);
    }
    protected override void Update() {}
    private void BomeDamager()
    {
        // 메소드 호출시 범위 감지
        Collider2D[] collider = Physics2D.OverlapCircleAll(explosionPoint.position, detectedRange, layerMask);
        foreach (Collider2D col in collider)
        {
            // 감지된 객체의 태그가 Enemy거나 Player면 
            if (col.CompareTag("Player") || col.CompareTag("Enemy"))
            {
                // 체력 관리 컴포넌트 호출
                HealthControlComponent hp = col.GetComponent<HealthControlComponent>();
                // 피격 메소드 호출
                hp.OnHit(explosionDamage);
            }
        }
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionPoint.position, detectedRange);
    }
}
