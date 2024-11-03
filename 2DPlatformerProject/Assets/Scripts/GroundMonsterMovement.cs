using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundMonsterMovement : DirectionHorizontalMovement
{
    // 감시할 레이어
    [SerializeField] protected LayerMask detectLayer;
    // 감시 거리
    [SerializeField] protected float detectionDistance;
    // 이동 방향 전환 체크 위치
    [SerializeField] protected Transform checkTransform;
    protected override void Move()
    {
        // * 레이캐스트 충돌 메소드
        // - RaycastHit2D 레이충돌검출정보 = Physics2D.Raycast(레이생성위치, 방향, 길이, 충돌레이어);

        // 방향 전환을 위해 시선 방향으로 레이캐스트 체크를 수행함
        RaycastHit2D raycastHit = Physics2D.Raycast(checkTransform.position, transform.right, detectionDistance, detectLayer);
        if(raycastHit.collider != null)
        {
            Flip();
        }
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
        //rigidbody2D.velocity = moveDirection * moveSpeed;
        animator?.SetFloat("Move", MoveSpeed);
    }
    // 충돌 레이 표시 기즈모
    private void OnDrawGizmosSelected()
    {
        // 라인 표시할 기즈모 색상을 설정함
        Gizmos.color = Color.yellow;

        // 레이캐스트와 같은 라인형태의 디버깅용 기즈모 선을 그려줌
        //  - Gizmos.DrawLine(그리는 시작 위치, 그리는 방향과 길이);
        Gizmos.DrawLine(checkTransform.position, checkTransform.position + transform.right * detectionDistance);
    }
    
}
