using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;

    private RectTransform dragTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Awake()
    {
        dragTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = dragTransform.position;

        // Disable raycasts on the item being dragged
        SetRaycastTarget(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragTransform.position = originalPosition;

        // Enable raycasts on the item being dragged
        SetRaycastTarget(true);

        if (InventoryHandler.Instance.CanEquip(item) && eventData.pointerEnter != null)
        {
            Transform dropSlot = eventData.pointerEnter.transform;

            if (dropSlot == InventoryHandler.Instance.shirtSlot && item.shirt)
            {
                // Check if there is already an item in the shirt slot
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    // Replace the existing shirt with the new shirt
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                // Equip the new shirt
                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
            else if (dropSlot == InventoryHandler.Instance.swordSlot && item.weapon)
            {
                // Check if there is already an item in the sword slot
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    // Replace the existing sword with the new sword
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                // Equip the new sword
                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
            else if (dropSlot == InventoryHandler.Instance.shieldSlot && item.shield)
            {
                // Check if there is already an item in the shield slot
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    // Replace the existing shield with the new shield
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                // Equip the new shield
                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
        }
    }

    private void SetRaycastTarget(bool enabled)
    {
        // Disable or enable raycast target on all child graphics of the item
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
}