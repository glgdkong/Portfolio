using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DirectionHorizontalMovement : DirectionMovement
{
    [SerializeField]private bool isRight = true;
    public bool IsRight { get => isRight; set => isRight = value; }
    protected Quaternion flipQuaternion;


    // Start is called before the first frame update
    protected void Start()
    {
        // ���ʹϾ� ���� ȸ���� �Է�
        flipQuaternion = transform.rotation;
        IsRight = flipQuaternion.y == 0 ? true : false;
    }
    protected virtual void Flip()
    {
        // ������ ���� �ִٸ� 180ȸ������ ��ȯ �ƴϸ� 0�� ��ȯ
        flipQuaternion = Quaternion.AngleAxis((IsRight ? 180f : 0f), Vector3.up);
        // ȸ������ ���ʹϾ� �Է�
        transform.rotation = flipQuaternion;
        // �Ұ� ��ȯ
        IsRight = !IsRight;
    }
    protected override void Move()
    {
        
    }
}
