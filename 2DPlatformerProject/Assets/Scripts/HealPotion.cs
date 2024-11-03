using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : UsingItems
{
    [SerializeField] protected int HealPower;
    public override void ItemUse(Collider2D collider)
    {
        // ������ ��� ������ Ʈ���
        if(isItemUseAble)
        {
            // �÷��̾��� ü�°��� ������Ʈ�� ������
            PlayerHealthControl hp = collider.GetComponent<PlayerHealthControl>();
            // ������ ������ŭ ü�� ���� �޼ҵ� ����
            hp.AddHealthPoint(HealPower);
            GameObject hs = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            Destroy(hs, 1f);
            // ���� ����
            Destroy(gameObject);
        }
    }


    public void InventoryUseItem()
    {

    }
}
