using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    // ������ ��� �̹���
    [SerializeField] private Image itemBackgroundImage;
    // ������ �̹���
    [SerializeField] private Image itemImage;
    // �κ��丮 UI
    private InventoryUI inventoryUI;
    // ������ ���� ����
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
