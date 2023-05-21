using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject prefab;
    public bool hasEquipTag, shirt, weapon, shield;

    [HideInInspector] public Transform equippedSlot;

    public void Equip(Transform slot)
    {
        equippedSlot = slot;
    }

    public void Unequip()
    {
        equippedSlot = null;
    }
}