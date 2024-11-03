
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : UsingItems
{
    [SerializeField] protected GameObject[] itemObject;
    [SerializeField] private Transform spawnPosition;
    public override void ItemUse(Collider2D collider)
    {
        if(isItemUseAble)
        {
            animator.SetTrigger("Open");
            isItemUseAble = false;
        }
    }
    [SerializeField]private void InstantiateItem()
    {
        int itemRamdomIndex = Random.Range(0, itemObject.Length);
        Instantiate(itemObject[itemRamdomIndex], spawnPosition.position, Quaternion.identity);
    }
    protected override void OnEnable()
    {
        transform.position = transform.position;
    }
}
