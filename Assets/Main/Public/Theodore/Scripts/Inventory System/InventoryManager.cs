// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A script used to display an inventory instance on the appropriate UI.

using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;

    private GameObject _entryPrefab;

    private void Awake()
    {
        // Get the prefab that will hold the invenotry entry from resources folder
        _entryPrefab = Resources.Load("itemSlotTemplate") as GameObject;
    }

    /// <summary>
    /// Displays an inventory instance in the given container transform.
    /// </summary>
    /// <param name="inventory"> The inventory instance to be displayed. </param>
    /// <param name="containerUI"> The transform under which the inventory entries will be created. </param>
    public void SetInventory(Inventory inventory, Transform containerUI)
    {
        _inventory = inventory;
        RefreshInventoryItems(containerUI);
    }

    // Creates the inventory entries and assigns the data from each Item into the respective ItemSlot
    private void RefreshInventoryItems(Transform containerUI)
    {
        foreach (Item item in _inventory.GetItemList())
        {
            GameObject newEntry = Instantiate(_entryPrefab, containerUI);

            // Assign ItemData to the newEntry
            newEntry.GetComponent<ItemSlot>().ItemData = item.ItemData;
            newEntry.GetComponent<ItemSlot>().Item = item;

            // Assign information of which inventory the Item is part of
            newEntry.GetComponent<ItemSlot>().Inventory = _inventory;

            // Assign the icon
            newEntry.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = item.ItemIcon;

            newEntry.SetActive(true);
        }
    }
}
