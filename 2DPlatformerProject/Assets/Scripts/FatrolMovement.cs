using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatrolMovement : DirectionHorizontalMovement
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    protected override void Move()
    {
        // ���� ����Ʈ ���̰� 0 �̸� ó�� �Ұ�
        if (waypoints.Length == 0) return;

        // ���� �̵� ��ġ�� ������
        Transform targetWatpoint = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWatpoint.position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, targetWatpoint.position) < 0.1f)
        {
            if(IsRight)
            {
                if (currentWaypointIndex == 0)
                { 
                    currentWaypointIndex++;
                    Flip();
                }
            }
            else // ������ �̵���
            {
                if (currentWaypointIndex == 1) 
                {
                    currentWaypointIndex--;
                    Flip();
                }
            }
        }


    }
}
