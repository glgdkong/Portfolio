using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatrolMovement : DirectionHorizontalMovement
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    protected override void Move()
    {
        // 웨이 포인트 길이가 0 이면 처리 불가
        if (waypoints.Length == 0) return;

        // 현재 이동 위치를 참조함
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
            else // 역방향 이동중
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
