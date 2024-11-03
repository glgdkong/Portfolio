using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTargetDirectionMovement : DirectionMovement
{
    protected Transform playerPosition;
    protected Vector3 targetToDirection;


    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.Find("EnemyBulletTargetPosition").GetComponent<Transform>();
    }

    private void Start()
    {
        if (playerPosition != null)
        {
            targetToDirection = playerPosition.position - transform.position;
            transform.right = targetToDirection.normalized;
        }
    }
    protected override void Move()
    {
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
    }
    protected override void OnPause()
    {
        isPaused = true;
    }
    protected override void OnResume()
    {
        isPaused = false;
    }
}
