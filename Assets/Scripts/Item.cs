using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject prefab;
    public bool hasEquipTag, shirt, weapon, shield;

    // Add any additional properties or methods for the item as needed
}