using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopObject;
    public GameObject menuObject;
    public GameObject menuStuff;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode cancelKey = KeyCode.Escape;
    public PlayerController player;

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
        player.canMove = false;
        menuObject.SetActive(true);
        shopObject.SetActive(false);

        Debug.Log("Shop menu opened");
    }

    private void CloseShopMenu()
    {
        isMenuOpen = false;
        player.canMove = true;
        menuStuff.SetActive(false);
        menuObject.SetActive(false);
        shopObject.SetActive(true);

        Debug.Log("Shop menu closed");
    }
}