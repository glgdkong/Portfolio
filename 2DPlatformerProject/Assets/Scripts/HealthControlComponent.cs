using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthControlComponent : MonoBehaviour
{
    [Range(-1, 3)]
    [SerializeField] protected int healthPoint;
    protected Animator animator;
    [SerializeField] protected bool hitAble = true; // �ǰ� ���� ó��
    protected SpriteRenderer spriteRenderer;
    public int HealthPoint { get => healthPoint;}

    protected virtual void Awake()
    {
        // �ִϸ����� ����
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void OnHit(int damage = 1);
    protected abstract void Death();

    // �ǰݽð� ���� �ڷ�ƾ
    protected virtual IEnumerator HitColorCoroutine(int damage)
    {
        // �ǰݰ��� ��Ȱ��ȭ
        hitAble = false;
        // ü�� ����Ʈ ����
        healthPoint -=damage;
        // ��������Ʈ ���� ����
        spriteRenderer.color = Color.red;
        // 0.1�� ����
        yield return new WaitForSeconds(0.1f);
        // ���� ���� ����
        spriteRenderer.color = Color.white;
        if (healthPoint < 0) // 0 �̸��̸�
        {
            // ���ó��
            Death();
            yield break; // �ڷ�ƾ ����
        }
        // 0.3�� ��
        yield return new WaitForSeconds(0.3f);
        // �ǰ� ���� Ȱ��ȭ
        hitAble = true;
    }


}
