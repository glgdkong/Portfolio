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

    // �κ��丮 UI ����
    public void OpenUI()
    {
        transform.parent.gameObject.SetActive(true);
    }
    // �κ��丮 UI �ݱ� ó��
    public void CloseUI()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void UseItem(HealPotion healPotion)
    {
        healPotion.InventoryUseItem();
    }
}
