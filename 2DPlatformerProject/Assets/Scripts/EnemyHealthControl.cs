using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : HealthControlComponent
{
    // �׾����� ������ ����Ʈ ������
    [SerializeField] protected GameObject effectPrefab;
    // �Ӹ��� ���� �� �ִ��� ����
    [SerializeField] private bool isHeadStep = true;
    public bool IsHeadStep => isHeadStep;
    // ��� ���� ��� ���ӿ�����Ʈ
    [SerializeField] private GameObject deathSoundPrefab;
    



    // �ǰ� �޼ҵ�
    public override void OnHit(int damage = 1)
    {
        // �ǰ� �����̸� �ڷ�ƾ ȣ��
        if(hitAble) StartCoroutine(HitColorCoroutine(damage));
    }
    // �ǰ� ���� �� ���� ���� �ڷ�ƾ
    protected override IEnumerator HitColorCoroutine(int damage)
    {
        // �ǰ� ��Ȱ��ȭ
        hitAble = false;
        // ü�� ����Ʈ ����
        healthPoint -= damage;
        if (healthPoint <= 0) // 0 �̸��̸�
        {
            // ���ó��
            Death();
            yield break;
        }
        // ���� ����
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.075f);
        // ���� ����
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        // �ǰ� Ȱ��ȭ
        hitAble = true;
    }

    protected override void Death()
    {
        // ����Ʈ ����
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        GameObject ds = Instantiate(deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(ds, 1f);
        // ���� ������Ʈ ����
        Destroy(gameObject);
    }
}
