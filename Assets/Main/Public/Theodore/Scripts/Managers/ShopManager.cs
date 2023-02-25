using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // A preset list of items the shop will hold. Can be set from the inspector.
    public List<ItemData> ItemList;

    // The inventory instance the shop will have.
    private Inventory _inventory;

    private InventoryManager _inventoryManager;

    private Transform _inventoryContainer;

    private void Awake()
    {
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        _inventoryContainer = GameObject.Find("ShopSlotContainer").transform;

        // Create inventory instance for the shop
        _inventory = new Inventory();

        // Load the preset items into the inventory instance
        LoadItems(ItemList);

        // Create the UI for the shop inventory
        _inventoryManager.SetInventory(_inventory, _inventoryContainer);
    }

    private void LoadItems(List<ItemData> itemDataList)
    {
        foreach (ItemData itemData in itemDataList)
        {
            _inventory.AddItem(new Item(itemData));
        }
    }

}
