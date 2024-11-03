using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지정 방향 이동 처리 컴포넌트
public class DirectionMovement : Movement
{
    protected virtual void Update()
    {
        if(isPaused) return;
        Move();
    }

    // 이동 처리 메소드
    protected override void Move()
    {
        // 이동속도 및 방향 설정
        rigidbody2D.velocity = MoveDirection.normalized * MoveSpeed;
    }
}