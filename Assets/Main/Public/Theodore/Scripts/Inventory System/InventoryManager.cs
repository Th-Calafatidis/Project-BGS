// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A script used to display an inventory instance on the appropriate UI.

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;

    public GameObject entryPrefab;


    private void Awake()
    {
        entryPrefab = Resources.Load("itemSlotTemplate") as GameObject;
    }

    public void SetInventory(Inventory inventory, Transform containerUI)
    {
        _inventory = inventory;
        RefreshInventoryItems(containerUI);
    }

    public void RefreshInventoryItems(Transform containerUI)
    {
        foreach (Item item in _inventory.GetItemList())
        {
            GameObject newEntry = Instantiate(entryPrefab, containerUI);

            // Assign ItemData to the newEntry
            newEntry.GetComponent<ItemSlot>().ItemData = item.ItemData;
            newEntry.GetComponent<ItemSlot>().Item = item;
            newEntry.GetComponent<ItemSlot>().Inventory = _inventory;
            newEntry.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = item.ItemIcon;

            newEntry.SetActive(true);
        }
    }
}
