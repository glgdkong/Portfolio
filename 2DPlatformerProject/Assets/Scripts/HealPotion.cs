using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : UsingItems
{
    [SerializeField] protected int HealPower;
    public override void ItemUse(Collider2D collider)
    {
        // 아이템 사용 가능이 트루면
        if(isItemUseAble)
        {
            // 플레이어의 체력관리 컴포넌트를 가져옴
            PlayerHealthControl hp = collider.GetComponent<PlayerHealthControl>();
            // 포션의 힐량만큼 체력 증가 메소드 실행
            hp.AddHealthPoint(HealPower);
            GameObject hs = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            Destroy(hs, 1f);
            // 포션 제거
            Destroy(gameObject);
        }
    }


    public void InventoryUseItem()
    {

    }
}
