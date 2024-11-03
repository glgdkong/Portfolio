using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBossBattle : GameManager
{
    // 레이어 참조
    [SerializeField] protected LayerMask layerMask;
    // 감지 범위 
    [SerializeField] protected float detectionDistance;
    // 감지레이어 위치 참조
    [SerializeField] protected Transform checkTransform;

    // 마지막 보스 게임오브젝트
    [SerializeField] protected GameObject lastBoss;

    [SerializeField] protected TextMeshProUGUI victoryCanvas;



    // 마지막 보스 사망 카운트 변수
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
                    boss.enabled = true; // 보스 활성화
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

    // 보스 사망 카운트 증가시킬 메소드
    public void LastBoosDeath()
    {
        ++lastBossDeathCount;
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
