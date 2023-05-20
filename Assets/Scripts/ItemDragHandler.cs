using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    [HideInInspector] public InventoryHandler inventoryHandler;

    private RectTransform dragTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Awake()
    {
        inventoryHandler = InventoryHandler.Instance;
        dragTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = dragTransform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragTransform.position = originalPosition;
        canvasGroup.blocksRaycasts = true;

        // Check if the item can be equipped and if the drag ended on an equip slot
        if (inventoryHandler.CanEquip(item) && eventData.pointerEnter != null)
        {
            Transform dropSlot = eventData.pointerEnter.transform;

            // Check if the drop target is an equip slot
            if (inventoryHandler.equipSlots.Contains(dropSlot))
            {
                // Equip the item
                inventoryHandler.EquipItem(item);
            }
        }
    }
}