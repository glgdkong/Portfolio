using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBossBattle : GameManager
{
    // ���̾� ����
    [SerializeField] protected LayerMask layerMask;
    // ���� ���� 
    [SerializeField] protected float detectionDistance;
    // �������̾� ��ġ ����
    [SerializeField] protected Transform checkTransform;

    // ������ ���� ���ӿ�����Ʈ
    [SerializeField] protected GameObject lastBoss;

    [SerializeField] protected TextMeshProUGUI victoryCanvas;



    // ������ ���� ��� ī��Ʈ ����
    protected int lastBossDeathCount;

    protected override void Start()
    {
        base.Start();
        StopAllCoroutines();
        StartCoroutine("StartBossBattleCoroutine");
    }

    IEnumerator StartBossBattleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            RaycastHit2D hit = Physics2D.Raycast(checkTransform.position, -checkTransform.transform.up, detectionDistance, layerMask);
            if (hit)
            {
                GroundMonsterMovement boss = lastBoss.gameObject.GetComponent<GroundMonsterMovement>();
                if (boss != null)
                {
                    boss.enabled = true; // ���� Ȱ��ȭ
                }
            }
            if (lastBossDeathCount >= 2)
            {
                
                victoryCanvas.gameObject?.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                while (true)
                {
                    yield return null;
                    if (Input.anyKeyDown)
                    {
                        victoryCanvas.gameObject.SetActive(false);
                        animator.SetTrigger("Loading");
                        playerHpControl.HitAble = false;
                        yield return new WaitForSeconds(1.2f); 
                        SceneManager.LoadScene(sceneIndex);
                        break;
                    }
                }
            }
        }
    }

    // ���� ��� ī��Ʈ ������ų �޼ҵ�
    public void LastBoosDeath()
    {
        ++lastBossDeathCount;
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
