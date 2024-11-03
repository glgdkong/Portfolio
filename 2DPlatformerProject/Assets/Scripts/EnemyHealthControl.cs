using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : HealthControlComponent
{
    // 죽었을때 나오는 이펙트 프리펩
    [SerializeField] protected GameObject effectPrefab;
    // 머리를 밟을 수 있는지 여부
    [SerializeField] private bool isHeadStep = true;
    public bool IsHeadStep => isHeadStep;
    // 사망 사운드 재생 게임오브젝트
    [SerializeField] private GameObject deathSoundPrefab;
    



    // 피격 메소드
    public override void OnHit(int damage = 1)
    {
        // 피격 가능이면 코루틴 호출
        if(hitAble) StartCoroutine(HitColorCoroutine(damage));
    }
    // 피격 지연 및 색상 변경 코루틴
    protected override IEnumerator HitColorCoroutine(int damage)
    {
        // 피격 비활성화
        hitAble = false;
        // 체력 포인트 감소
        healthPoint -= damage;
        if (healthPoint <= 0) // 0 미만이면
        {
            // 사망처리
            Death();
            yield break;
        }
        // 색상 변경
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.075f);
        // 색상 복원
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        // 피격 활성화
        hitAble = true;
    }

    protected override void Death()
    {
        // 이펙트 생성
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        GameObject ds = Instantiate(deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(ds, 1f);
        // 게임 오브젝트 삭제
        Destroy(gameObject);
    }
}
