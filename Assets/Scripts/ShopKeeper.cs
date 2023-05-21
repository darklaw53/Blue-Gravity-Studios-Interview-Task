using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopObject;
    public GameObject menuObject;
    public GameObject inventoryObject;
    public GameObject menuStuff;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode cancelKey = KeyCode.Escape;

    private bool isPlayerInsideTrigger = false;
    private bool isMenuOpen = false;
    private bool wasPlayerControllable = false;

    private void Update()
    {
        if (isPlayerInsideTrigger)
        {
            if (Input.GetKeyDown(interactKey) && !isMenuOpen)
            {
                OpenShopMenu();
            }
            else if ((Input.GetKeyDown(cancelKey) || Input.GetKeyDown(interactKey)) && isMenuOpen)
            {
                CloseShopMenu();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
            shopObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
            shopObject.SetActive(false);
            if (isMenuOpen)
            {
                CloseShopMenu();
            }
        }
    }

    private void OpenShopMenu()
    {
        isMenuOpen = true;
        PlayerController.Instance.canMove = false;
        menuObject.SetActive(true);
        shopObject.SetActive(false);
        inventoryObject.SetActive(false);
    }

    private void CloseShopMenu()
    {
        isMenuOpen = false;
        PlayerController.Instance.canMove = true;
        menuStuff.SetActive(false);
        menuObject.SetActive(false);
        shopObject.SetActive(true);
    }
}