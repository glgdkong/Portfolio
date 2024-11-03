using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossPaseTwoHealthControl : EnemyHealthControl
{

    [SerializeField] private Rigidbody2D r2d;
    private Vector3 vector;
    [SerializeField] private float jumpSpeed;
    [SerializeField] protected GameObject gameManager = null;

    protected override void Awake()
    {
        base.Awake();
        hitAble = false;
        gameManager = GameObject.FindWithTag("GameController");
    }
    IEnumerator Start()
    {
        vector = new Vector3(transform.right.x, transform.up.y, 0f);
        r2d.velocity = vector.normalized * jumpSpeed;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(0.7f);
        hitAble = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    protected override void Death()
    {
        base.Death();
        gameManager.GetComponent<StartBossBattle>().LastBoosDeath();
    }
}
