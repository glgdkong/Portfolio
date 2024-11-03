using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : DirectionMovement
{
    // 이동 범위
    [SerializeField] protected float moveDistance;
    // 플랫폼 초기 위치
    protected Vector3 startPosition;
    private Vector3 movePosition;
    [SerializeField] protected bool isVertical;
    private float currentTime; // 시간 추적

    private void Start()
    {
        startPosition = transform.position;
        movePosition = new Vector3(0,0,0);
        
    }
    // 이동 처리
    protected override void Move()
    {
        currentTime += Time.deltaTime; // 퍼즈가 아닐 때만 시간 업데이트

        // 수직 이동 처리
        float move = Mathf.Sin(currentTime * moveSpeed) * moveDistance;
        if (isVertical)
        {
            movePosition.y = move;
            transform.position = startPosition + movePosition;
        }
        else
        {
            movePosition.x = move;
            transform.position = startPosition + movePosition;
        }
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
