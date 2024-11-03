using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInputAttack : AttackComopnent
{
    private const float attackDelayTime = 0.5f;
    protected bool isAttackAble = true;
    [SerializeField] protected Transform attackPosition;
    [SerializeField] protected float attackRange;
    [SerializeField] protected GameObject slashEffect;

    public bool IsAttackAble { get => isAttackAble; set => isAttackAble = value; }
    protected virtual void Update()
    {
        if(isPaused) return;

        Attack();
        float attackAbleTime = Time.deltaTime;
        if (!IsAttackAble && attackAbleTime >= attackDelayTime)
        {
            SetAttackAble();
            attackAbleTime = 0f;
        }
    }

    protected override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.D) && IsAttackAble)
        {
            animator.SetBool("IsAttack", IsAttackAble);
            animator.SetTrigger("Attack");
            IsAttackAble = false;
        }
    }
    public void AttackPointDetected()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, layerMask);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyHealthControl enemy = collider.GetComponent<EnemyHealthControl>();
                enemy.OnHit();
            }
        }
    }

    protected void SetAttackAble()
    {
        IsAttackAble = true;
    }
    
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }*/

    private void SlashEffectInstatiateAniamtorEvent()
    {
       GameObject slash = Instantiate(slashEffect, attackPosition);
       slash.transform.position = attackPosition.position;
       slash.transform.rotation = attackPosition.rotation;
    }
}
