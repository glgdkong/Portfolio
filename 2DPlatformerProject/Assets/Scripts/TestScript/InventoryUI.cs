using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] images; 

    void Update()
    {
        
    }

    // 인벤토리 UI 열기
    public void OpenUI()
    {
        transform.parent.gameObject.SetActive(true);
    }
    // 인벤토리 UI 닫기 처리
    public void CloseUI()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void UseItem(HealPotion healPotion)
    {
        healPotion.InventoryUseItem();
    }
}
