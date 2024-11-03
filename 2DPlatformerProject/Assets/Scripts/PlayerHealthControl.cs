using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthControl : HealthControlComponent
{
    protected  bool isDead = false; // ĳ���� ���� Ȯ��
    public  bool IsDead { get => isDead; set => isDead = value; }
    [SerializeField] protected Rigidbody2D r2d;
    // �̹��� �迭
    [SerializeField] private Image[] heartImages;
    // ��������Ʈ �迭
    [SerializeField] private Sprite[] heartSprites;

    [SerializeField] private Vector2 heathPointMinMax;
    public bool HitAble { get => hitAble; set => hitAble = value; } // �ǰ� ���� ������Ƽ

    private void Start()
    {
        healthPoint = GameManager.PlayerHp > 0 ? GameManager.PlayerHp : 0;

        for (int i = 0; i < healthPoint; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
    protected override void Death()
    {
        // ��� �޼ҵ� ȣ��� ���ó��
        IsDead = true;
        // �ִϸ����� bool�� ����
        animator.SetBool("IsDead", IsDead);
        // �÷��̾� �ӷ� ����
        r2d.velocity = Vector3.zero;
    }

    public override void OnHit(int damage = 1)
    {
        // �ǰ��� �����ҽ�
        if (hitAble)
        {
            // ü�� ����Ʈ�� 0 �̻��̸�
            if (healthPoint >= 0)
            {
                // �ǰ� �ڷ�ƾ ����
                StartCoroutine(HitColorCoroutine(damage));
            }
        }
    }
    // �ǰݽð� ���� �ڷ�ƾ
    protected override IEnumerator HitColorCoroutine(int damage)
    {
        hitAble = false;
        // ü�� ����Ʈ ����
        healthPoint -= damage;
        // �÷��̾� ���� ����
        spriteRenderer.color = Color.red;
        // �����浹 ��Ȱ��ȭ
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        // 0.1�� ����
        yield return new WaitForSeconds(0.1f);
        // �÷��̾� ���� ����
        spriteRenderer.color = Color.white;
        for (int i = 0; i < 3 - healthPoint && i < heartImages.Length; i++)
        {
            heartImages[heartImages.Length - 1 - i].sprite = heartSprites[1];
        }
        if (healthPoint < 0) // 0 �̸��̸�
        {
            // ���ó��
            Death();
            // ���Ϳ� �����浹 ����
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            // �ڷ�ƾ ����
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        // �����浹 Ȱ��ȭ
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        hitAble = true;
    }
    public void AddHealthPoint(int healPower)
    {
        int overflowHp = healPower - healthPoint;
        healthPoint += healPower;
        healthPoint = Mathf.Clamp(healthPoint, (int)heathPointMinMax.x, (int)heathPointMinMax.y);
        for (int i = 0; i < healthPoint; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
    
}
