using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;

    private RectTransform dragTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private int originalSortingOrder;
    private Camera mainCamera;
    private Graphic[] graphics;

    private void Awake()
    {
        dragTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCamera = Camera.main;
        graphics = GetComponentsInChildren<Graphic>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = dragTransform.position;
        originalSortingOrder = GetComponent<Canvas>().sortingOrder;

        // Disable raycasts on the item being dragged
        SetRaycastTarget(false);

        // Increase the sorting order to ensure visibility over other UI elements
        GetComponent<Canvas>().sortingOrder = 999;

        // Move the item closer to the camera to ensure visibility
        Vector3 dragPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        dragPosition.z = 0f;
        dragTransform.position = dragPosition;

        // Adjust the sorting order of child graphics to ensure visibility
        foreach (Graphic graphic in graphics)
        {
            graphic.canvas.overrideSorting = true;
            graphic.canvas.sortingOrder = GetComponent<Canvas>().sortingOrder;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dragPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        dragPosition.z = 0f;
        dragTransform.position = dragPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragTransform.position = originalPosition;

        // Restore the original sorting order
        GetComponent<Canvas>().sortingOrder = originalSortingOrder;

        // Enable raycasts on the item being dragged
        SetRaycastTarget(true);

        // Restore the original sorting order of child graphics
        foreach (Graphic graphic in graphics)
        {
            graphic.canvas.overrideSorting = false;
        }

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