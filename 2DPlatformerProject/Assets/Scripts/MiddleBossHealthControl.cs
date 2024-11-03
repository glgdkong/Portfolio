using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossHealthControl : EnemyHealthControl
{
    [SerializeField] protected GameObject blockStone;
    protected override void Death()
    {
        base.Death();
        blockStone.GetComponent<Animator>().SetTrigger("Delet");
    }
}
