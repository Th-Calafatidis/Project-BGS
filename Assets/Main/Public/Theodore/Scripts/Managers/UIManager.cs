// Date 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A script that handles the UI

using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool invUiActive = false;
    public static bool shopUiActive = false;

    private void Update()
    {
        // Update the gold amount display in the UI
        DatabaseManager.Instance.PlayerGoldCounter.text = PlayerManager.Instance.PlayerGoldAmount.ToString() + " GP";
        DatabaseManager.Instance.ShopGoldCounter.text = ShopManager.Instance.ShopGoldAmount.ToString() + " GP";

        // Toggle Inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Toggle Shop UI
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleShop();
        }
    }

    /// <summary>
    /// Toggles the Inventory panel On / Off
    /// </summary>
    public void ToggleInventory()
    {
        if (!DatabaseManager.Instance.InventoryPanel.gameObject.activeSelf)
        {
            DatabaseManager.Instance.InventoryPanel.gameObject.SetActive(true);

            invUiActive = true;
        }
        else
        {
            DatabaseManager.Instance.InventoryPanel.gameObject.SetActive(false);

            invUiActive = false;
        }
    }

    /// <summary>
    /// Toggles the Shop and Inventory panels On / Off
    /// </summary>
    public void ToggleShop()
    {
        if (!DatabaseManager.Instance.ShopPanel.gameObject.activeSelf)
        {
            DatabaseManager.Instance.ShopPanel.gameObject.SetActive(true);
            DatabaseManager.Instance.InventoryPanel.gameObject.SetActive(true);

            shopUiActive = true;
            invUiActive = true;
        }
        else
        {
            DatabaseManager.Instance.ShopPanel.gameObject.SetActive(false);

            shopUiActive = false;

        }
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExtiGame()
    {
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#endif

        Application.Quit();
    }
}
