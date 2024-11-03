using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossPaseOneHealthControl : EnemyHealthControl
{
    [SerializeField] private GameObject[] littleBoss;
    int randomNum;
    private Quaternion quaternion = Quaternion.identity;
    [SerializeField] private Transform spawnPosition;

    protected override void Death()
    {
        base.Death();

        quaternion.y = (randomNum == 0 ? 180 : 0);
        Instantiate(littleBoss[0], spawnPosition.position, quaternion);
        quaternion.y = (randomNum == 1 ? 180 : 0);
        Instantiate(littleBoss[1], transform.position, quaternion);
    }

}
