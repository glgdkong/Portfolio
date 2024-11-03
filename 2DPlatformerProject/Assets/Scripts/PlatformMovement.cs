using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : DirectionMovement
{
    // �̵� ����
    [SerializeField] protected float moveDistance;
    // �÷��� �ʱ� ��ġ
    protected Vector3 startPosition;
    private Vector3 movePosition;
    [SerializeField] protected bool isVertical;
    private float currentTime; // �ð� ����

    private void Start()
    {
        startPosition = transform.position;
        movePosition = new Vector3(0,0,0);
        
    }
    // �̵� ó��
    protected override void Move()
    {
        currentTime += Time.deltaTime; // ��� �ƴ� ���� �ð� ������Ʈ

        // ���� �̵� ó��
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
