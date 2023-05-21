using UnityEngine;
using TMPro;

public class PurchaseItem : MonoBehaviour
{
    public int price = 10;
    public TextMeshProUGUI totalGoldText;
    public GameObject itemGO;
    public bool soldOut;

    public AudioSource audioSource;
    public AudioClip cashRegisterSound;

    public void BuyItem()
    {
        int totalGold = int.Parse(totalGoldText.text);

        if (totalGold >= price && !soldOut)
        {
            soldOut = true;
            totalGold -= price;
            totalGoldText.text = totalGold.ToString();

            PlayCashRegisterSound();

            // Add the item to the inventory
            Item item = itemGO.GetComponent<ItemDragHandler>().item;
            InventoryHandler.Instance.AddItemToInventory(item);
        }
    }

    private void PlayCashRegisterSound()
    {
        audioSource.PlayOneShot(cashRegisterSound);
    }
}