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
        // 쿼터니언에 현재 회전값 입력
        flipQuaternion = transform.rotation;
        IsRight = flipQuaternion.y == 0 ? true : false;
    }
    protected virtual void Flip()
    {
        // 우측을 보고 있다면 180회전값을 반환 아니면 0을 반환
        flipQuaternion = Quaternion.AngleAxis((IsRight ? 180f : 0f), Vector3.up);
        // 회전값에 쿼터니언 입력
        transform.rotation = flipQuaternion;
        // 불값 전환
        IsRight = !IsRight;
    }
    protected override void Move()
    {
        
    }
}
