using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �̵� ó�� ������Ʈ
public class DirectionMovement : Movement
{
    protected virtual void Update()
    {
        if(isPaused) return;
        Move();
    }

    // �̵� ó�� �޼ҵ�
    protected override void Move()
    {
        // �̵��ӵ� �� ���� ����
        rigidbody2D.velocity = MoveDirection.normalized * MoveSpeed;
    }
}