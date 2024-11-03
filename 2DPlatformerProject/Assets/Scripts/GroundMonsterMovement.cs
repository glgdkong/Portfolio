using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundMonsterMovement : DirectionHorizontalMovement
{
    // ������ ���̾�
    [SerializeField] protected LayerMask detectLayer;
    // ���� �Ÿ�
    [SerializeField] protected float detectionDistance;
    // �̵� ���� ��ȯ üũ ��ġ
    [SerializeField] protected Transform checkTransform;
    protected override void Move()
    {
        // * ����ĳ��Ʈ �浹 �޼ҵ�
        // - RaycastHit2D �����浹�������� = Physics2D.Raycast(���̻�����ġ, ����, ����, �浹���̾�);

        // ���� ��ȯ�� ���� �ü� �������� ����ĳ��Ʈ üũ�� ������
        RaycastHit2D raycastHit = Physics2D.Raycast(checkTransform.position, transform.right, detectionDistance, detectLayer);
        if(raycastHit.collider != null)
        {
            Flip();
        }
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
        //rigidbody2D.velocity = moveDirection * moveSpeed;
        animator?.SetFloat("Move", MoveSpeed);
    }
    // �浹 ���� ǥ�� �����
    private void OnDrawGizmosSelected()
    {
        // ���� ǥ���� ����� ������ ������
        Gizmos.color = Color.yellow;

        // ����ĳ��Ʈ�� ���� ���������� ������ ����� ���� �׷���
        //  - Gizmos.DrawLine(�׸��� ���� ��ġ, �׸��� ����� ����);
        Gizmos.DrawLine(checkTransform.position, checkTransform.position + transform.right * detectionDistance);
    }
    
}
