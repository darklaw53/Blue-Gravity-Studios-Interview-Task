using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log("onbegindrag");
        originalPosition = dragTransform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("ondrag");
        dragTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("onenddrag");
        dragTransform.position = originalPosition;
        canvasGroup.blocksRaycasts = true;

        // Check if the item can be equipped and if the drag ended on an equip slot
        if (InventoryHandler.Instance.CanEquip(item) && eventData.pointerEnter != null)
        {
            Transform dropSlot = eventData.pointerEnter.transform;

            // Check if the drop target is an equip slot
            if (InventoryHandler.Instance.shirtSlot == dropSlot)
            {
                // Equip the item
                InventoryHandler.Instance.EquipItem(item, dropSlot, "YellowShirt");
            }
        }
    }
}