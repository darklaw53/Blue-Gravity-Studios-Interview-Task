using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject shopObject;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private GameObject menuStuff;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cashRegisterSound;

    private bool isPlayerInsideTrigger = false;
    private bool isMenuOpen = false;

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

    private void PlayCashRegisterSound()
    {
        audioSource.clip = cashRegisterSound;
        audioSource.Play();
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