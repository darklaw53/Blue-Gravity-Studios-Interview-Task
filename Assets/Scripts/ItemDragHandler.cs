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
        SetRaycastTarget(false);

        Vector3 dragPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        dragPosition.z = 0f;
        dragTransform.position = dragPosition;
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

        SetRaycastTarget(true);

        if (InventoryHandler.Instance.CanEquip(item) && eventData.pointerEnter != null)
        {
            Transform dropSlot = eventData.pointerEnter.transform;

            if (dropSlot == InventoryHandler.Instance.shirtSlot && item.shirt)
            {
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
            else if (dropSlot == InventoryHandler.Instance.swordSlot && item.weapon)
            {
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
            else if (dropSlot == InventoryHandler.Instance.shieldSlot && item.shield)
            {
                Item existingItem = InventoryHandler.Instance.GetItemInSlot(dropSlot);

                if (existingItem != null)
                {
                    InventoryHandler.Instance.UnEquipItem(existingItem);
                }

                InventoryHandler.Instance.EquipItem(item, dropSlot, item.itemName);
            }
        }
    }

    private void SetRaycastTarget(bool enabled)
    {
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
}