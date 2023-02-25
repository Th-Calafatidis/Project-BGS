using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    private GameObject _inventoryUI;
    private GameObject _shopUI;

    public static bool invUiActive = true;
    public static bool shopUiActive = true;

    private void Awake()
    {
        _inventoryUI = GameObject.Find("UI_Inventory");
        _shopUI = GameObject.Find("UI_Shop");
    }

    private void Update()
    {
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

    void ToggleInventory()
    {
        if (_inventoryUI.gameObject.activeSelf)
        {
            _inventoryUI.gameObject.SetActive(false);

            invUiActive = false;
        }
        else
        {
            _inventoryUI.gameObject.SetActive(true);

            invUiActive = true;
        }
    }

    void ToggleShop()
    {
        if (_shopUI.gameObject.activeSelf)
        {
            _shopUI.gameObject.SetActive(false);

            shopUiActive = false;
        }
        else
        {
            _shopUI.gameObject.SetActive(true);

            shopUiActive = true;
        }
    }
}
