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
        // �޼ҵ� ȣ��� ���� ����
        Collider2D[] collider = Physics2D.OverlapCircleAll(explosionPoint.position, detectedRange, layerMask);
        foreach (Collider2D col in collider)
        {
            // ������ ��ü�� �±װ� Enemy�ų� Player�� 
            if (col.CompareTag("Player") || col.CompareTag("Enemy"))
            {
                // ü�� ���� ������Ʈ ȣ��
                HealthControlComponent hp = col.GetComponent<HealthControlComponent>();
                // �ǰ� �޼ҵ� ȣ��
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
