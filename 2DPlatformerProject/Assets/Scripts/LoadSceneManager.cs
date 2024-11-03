using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : GameManager
{
    // 레이어 참조
    [SerializeField] protected LayerMask layerMask;
    // 감지 범위 
    [SerializeField] protected float detectionDistance;
    // 감지레이어 위치 참조
    [SerializeField] protected Transform checkTransform;

    protected override void Start()
    {
        base.Start();
        StopAllCoroutines();
        StartCoroutine("NextSceneLoadingCoroutine");
    }

    protected void Update()
    {
        if (playerDetected)
        {
            if (playerInputMovement.IsGrounded)
            {
                playerInputMovement.Rigidbody2D.velocity = Vector3.right * 4.8f; 
            }
            else
            {
                playerInputMovement.IsGrounded = playerInputMovement.IsGroundedMethod();
                playerInputMovement.Rigidbody2D.velocity = -Vector3.up * 10f; 
            }
            playerHpControl.gameObject.GetComponent<Animator>().SetFloat("Move", Mathf.Abs(1f));
            //healthControl.transform.Translate(playerInputMovement.transform.right * 4.8f * Time.deltaTime);
        }
    }

    IEnumerator NextSceneLoadingCoroutine()
    {
        while (true)
        {
            yield return null;
            RaycastHit2D hit = Physics2D.Raycast(checkTransform.position, -checkTransform.transform.up, detectionDistance, layerMask);
            if (hit)
            {
                animator.SetTrigger("Loading");
                pauseGameManager.IsLoading = true;
                playerHp = playerHpControl.HealthPoint;
                playerDetected = true;
                playerInputMovement.IsLoading = true;
            }
            if (playerDetected)
            {
                yield return new WaitForSeconds(1f);
                playerInputMovement.IsLoading = false;
                SceneManager.LoadScene(sceneIndex);
                // 코루틴 종료
                yield break;
            }
        }
    }

    // 충돌 레이 표시 기즈모
    private void OnDrawGizmosSelected()
    {
        // 라인 표시할 기즈모 색상을 설정함
        Gizmos.color = Color.yellow;

        // 레이캐스트와 같은 라인형태의 디버깅용 기즈모 선을 그려줌
        //  - Gizmos.DrawLine(그리는 시작 위치, 그리는 방향과 길이);
        Gizmos.DrawLine(checkTransform.position, checkTransform.position + -transform.up * detectionDistance);
    }
}
