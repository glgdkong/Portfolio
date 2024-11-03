using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : GameManager
{
    // ���̾� ����
    [SerializeField] protected LayerMask layerMask;
    // ���� ���� 
    [SerializeField] protected float detectionDistance;
    // �������̾� ��ġ ����
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
                // �ڷ�ƾ ����
                yield break;
            }
        }
    }

    // �浹 ���� ǥ�� �����
    private void OnDrawGizmosSelected()
    {
        // ���� ǥ���� ����� ������ ������
        Gizmos.color = Color.yellow;

        // ����ĳ��Ʈ�� ���� ���������� ������ ����� ���� �׷���
        //  - Gizmos.DrawLine(�׸��� ���� ��ġ, �׸��� ����� ����);
        Gizmos.DrawLine(checkTransform.position, checkTransform.position + -transform.up * detectionDistance);
    }
}
