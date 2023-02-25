// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A class holding data relative to the player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // The player's inventory
    private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } set { _inventory = value; } }

    private Transform _inventoryContainer;

    private InventoryManager _inventoryUI;

    private void Awake()
    {
        // Create a new inventory instance for the player when game starts
        _inventory = new Inventory();

        LoadItems();

        _inventoryContainer = GameObject.Find("InventorySlotContainer").transform;

        _inventoryUI = GetComponent<InventoryManager>();

        _inventoryUI.SetInventory(_inventory, _inventoryContainer);

    }

    // Debugging

    public List<ItemData> ItemList;

    public void LoadItems()
    {
        foreach(ItemData itemData in ItemList)
        {
            _inventory.AddItem(new Item(itemData));
        }
    }
}
