using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Animator animator;


    void Update()
    {
        
    }


    public void UseItem(HealPotion healPotion)
    {
        healPotion.InventoryUseItem();
    }
}
