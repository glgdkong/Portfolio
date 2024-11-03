using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    // 아이템 배경 이미지
    [SerializeField] private Image itemBackgroundImage;
    // 아이템 이미지
    [SerializeField] private Image itemImage;
    // 인벤토리 UI
    private InventoryUI inventoryUI;
    // 아이템 선택 여부
    [SerializeField] private bool isSelected = false;
    [SerializeField] private Color32 selectColor;
    [SerializeField] private Color32 deSelectColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
