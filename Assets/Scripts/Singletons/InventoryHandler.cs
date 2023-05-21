using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : Singleton<InventoryHandler>
{
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private Transform gridParent;
    [SerializeField] private List<Item> inventoryItems = new List<Item>();
    [SerializeField] private Transform swordSlot;
    [SerializeField] private Transform shieldSlot;
    [SerializeField] private Transform shirtSlot;
    [SerializeField] private Item shirt;
    [SerializeField] private Item sword;
    [SerializeField] private Item shield;
    [SerializeField] private GameObject startingOutfit;

    private bool isInventoryOpen = false;
    private List<Transform> itemSlots = new List<Transform>();

    private void Start()
    {
        InitializeInventory();
        EquipStartingOutfit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryScreen();
        }
    }

    private void InitializeInventory()
    {
        inventoryScreen.SetActive(false);
        GetItemSlots();
    }

    private void EquipStartingOutfit()
    {
        Item startingItem = startingOutfit.GetComponent<ItemDragHandler>().item;
        EquipItem(startingItem, shirtSlot, "GreenShirt");
        AddItemToInventory(startingItem);
    }

    public void ToggleInventoryScreen()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryScreen.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            DisplayInventoryItems();
        }
    }

    public void AddItemToInventory(Item item)
    {
        inventoryItems.Add(item);
    }

    private void DisplayInventoryItems()
    {
        ClearGrid();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            Item item = inventoryItems[i];
            Transform slot = itemSlots[i];

            GameObject itemPrefab = Instantiate(item.prefab, slot);
            itemPrefab.transform.localPosition = Vector3.zero;

            Image image = itemPrefab.GetComponent<Image>();
            image.raycastTarget = true;
        }
    }

    public bool CanEquip(Item item)
    {
        return item.hasEquipTag;
    }

    public void EquipItem(Item item, Transform spot, string type)
    {
        Transform existingItem = spot.GetChild(0);
        if (existingItem != null)
        {
            Destroy(existingItem.gameObject);
        }

        GameObject itemPrefab = Instantiate(item.prefab, spot);
        itemPrefab.transform.localPosition = Vector3.zero;

        Image image = itemPrefab.GetComponent<Image>();
        image.raycastTarget = false;

        switch (type)
        {
            case "YellowShirt":
                OutfitSwap.Instance.SwapOutfit("Yellow");
                break;
            case "MagentaShirt":
                OutfitSwap.Instance.SwapOutfit("Magenta");
                break;
            case "GreenShirt":
                OutfitSwap.Instance.SwapOutfit("Green");
                break;
        }
    }

    public void UnEquipItem(Item item)
    {
        if (item != null && item.equippedSlot != null)
        {
            Destroy(item.equippedSlot.gameObject);
            item.equippedSlot = null;
        }
    }

    public Item GetItemInSlot(Transform slot)
    {
        if (slot == null)
        {
            Debug.LogWarning("Slot transform is null.");
            return null;
        }

        if (slot.childCount > 0)
        {
            Item item = slot.GetChild(0).GetComponent<ItemDragHandler>()?.item;
            return item;
        }

        return null;
    }

    private void GetItemSlots()
    {
        itemSlots.Clear();

        foreach (Transform child in gridParent)
        {
            if (child != gridParent)

            {
                itemSlots.Add(child);
            }
        }
    }

    private void ClearGrid()
    {
        foreach (Transform slot in itemSlots)
        {
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }
}
