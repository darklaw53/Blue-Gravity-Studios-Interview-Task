using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : Singleton<InventoryHandler>
{
    public GameObject inventoryScreen;
    public Transform gridParent;
    public List<Item> inventoryItems = new List<Item>();
    //public List<Transform> equipSlots = new List<Transform>();
    public Transform shirtSlot;
    public Transform swordSlot;
    public Transform shieldSlot;
    public List shirt, sword, shield;

    public Transform shirtSpot, swordSlot, shieldSlot;
    public GameObject YellowShirt, GreenShirt, MagentaShirt, Sword, Shield;

    private bool isInventoryOpen = false;
    private List<Transform> itemSlots = new List<Transform>();

    private void Start()
    {
        // Hide the inventory screen initially
        inventoryScreen.SetActive(false);

        // Get all item slots in the grid parent
        foreach (Transform child in gridParent.GetComponentsInChildren<Transform>(true))
        {
            if (child != gridParent)
            {
                itemSlots.Add(child);
            }
        }
    }

    private void Update()
    {
        // Open or close the inventory screen
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryScreen();
        }
    }

    public void ToggleInventoryScreen()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryScreen.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            // Display inventory items on the grid
            DisplayInventoryItems();
        }
    }

    public void AddItemToInventory(Item item)
    {
        inventoryItems.Add(item);
    }

    private void DisplayInventoryItems()
    {
        // Clear all existing items from the grid
        ClearGrid();

        // Display the inventory items on the grid
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            Item item = inventoryItems[i];

            // Create a new item slot
            Transform slot = itemSlots[i];

            // Instantiate and position the item's prefab in the slot
            GameObject itemPrefab = Instantiate(item.prefab, slot);
            itemPrefab.transform.localPosition = Vector3.zero;
            /*
            // Attach the ItemDragHandler script to the item's prefab
            ItemDragHandler dragHandler = itemPrefab.AddComponent<ItemDragHandler>();
            dragHandler.item = item;
            dragHandler.inventoryHandler = this; */
        }
    }

    public bool CanEquip(Item item)
    {
        return item.hasEquipTag;
    }

    public void EquipItem(Item item, Transform spot, string type)
    {
        // Check if there is an existing object in the equip slot
        if (spot.childCount > 0)
        {
            // Remove the existing object from the equip slot
            Transform existingItem = spot.GetChild(0);
            Destroy(existingItem.gameObject);
        }

        // Instantiate and position the item's prefab in the equip slot
        GameObject itemPrefab = Instantiate(item.prefab, spot);
        itemPrefab.transform.localPosition = Vector3.zero;

        if (type = "YellowShirt")
        {
            if (shirtSlot.childCount > 0)
            {
                // Remove the existing object from the equip slot
                Transform existingItem = shirtSlot.GetChild(0);
                Destroy(existingItem.gameObject);
            }

            GameObject itemPrefab = Instantiate(YellowShirt, shirtSpot);
            itemPrefab.transform.localPosition = Vector3.zero;
        }
    }
    /*
    private Transform GetAvailableEquipSlot()
    {
        foreach (Transform slot in equipSlots)
        {
            // If the slot is empty, return it
            if (slot.childCount == 0)
            {
                return slot;
            }
        }

        return null; // No available equip slot found
    }*/

    private void ClearGrid()
    {
        foreach (Transform slot in itemSlots)
        {
            // Destroy any existing item in the slot
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }
}
