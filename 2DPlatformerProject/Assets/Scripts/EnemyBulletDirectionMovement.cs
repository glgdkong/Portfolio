using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullerDirectionMovement : DirectionMovement
{
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
