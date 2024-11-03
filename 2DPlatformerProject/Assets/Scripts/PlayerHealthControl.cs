using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthControl : HealthControlComponent
{
    protected  bool isDead = false; // 캐릭터 죽음 확인
    public  bool IsDead { get => isDead; set => isDead = value; }
    [SerializeField] protected Rigidbody2D r2d;
    // 이미지 배열
    [SerializeField] private Image[] heartImages;
    // 스프라이트 배열
    [SerializeField] private Sprite[] heartSprites;

    [SerializeField] private Vector2 heathPointMinMax;
    public bool HitAble { get => hitAble; set => hitAble = value; } // 피격 가능 프로퍼티

    private void Start()
    {
        healthPoint = GameManager.PlayerHp > 0 ? GameManager.PlayerHp : 0;

        for (int i = 0; i < healthPoint; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
    protected override void Death()
    {
        // 사망 메소드 호출시 사망처리
        IsDead = true;
        // 애니메이터 bool값 적용
        animator.SetBool("IsDead", IsDead);
        // 플레이어 속력 제거
        r2d.velocity = Vector3.zero;
    }

    public override void OnHit(int damage = 1)
    {
        // 피격이 가능할시
        if (hitAble)
        {
            // 체력 포인트가 0 이상이면
            if (healthPoint >= 0)
            {
                // 피격 코루틴 실행
                StartCoroutine(HitColorCoroutine(damage));
            }
        }
    }
    // 피격시간 지연 코루틴
    protected override IEnumerator HitColorCoroutine(int damage)
    {
        hitAble = false;
        // 체력 포인트 감소
        healthPoint -= damage;
        // 플레이어 색상 변경
        spriteRenderer.color = Color.red;
        // 물리충돌 비활성화
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        // 0.1초 지연
        yield return new WaitForSeconds(0.1f);
        // 플레이어 색상 복원
        spriteRenderer.color = Color.white;
        for (int i = 0; i < 3 - healthPoint && i < heartImages.Length; i++)
        {
            heartImages[heartImages.Length - 1 - i].sprite = heartSprites[1];
        }
        if (healthPoint < 0) // 0 미만이면
        {
            // 사망처리
            Death();
            // 몬스터와 물리충돌 해제
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            // 코루틴 종료
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        // 물리충돌 활성화
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        hitAble = true;
    }
    public void AddHealthPoint(int healPower)
    {
        int overflowHp = healPower - healthPoint;
        healthPoint += healPower;
        healthPoint = Mathf.Clamp(healthPoint, (int)heathPointMinMax.x, (int)heathPointMinMax.y);
        for (int i = 0; i < healthPoint; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
    
}
