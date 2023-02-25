// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: The base class from which all inventory instances will be created.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // A list of ItemData that the inventory instance will hold
    private List<Item> _itemList;

    private Transform _inventoryContainer;

    /// <summary>
    /// A constructor used to create a new inventory instance.
    /// </summary>
    public Inventory(Transform inventoryContainer)
    {
        // Initialise a new ItemData list for this inventory instance to use
        _itemList = new List<Item>();

        _inventoryContainer = inventoryContainer;
    }

    /// <summary>
    /// Adds an item to the inventory instance.
    /// </summary>
    /// <param name="itemData"> The ItemData to be added. </param>
    public void AddItem(Item item)
    {
        _itemList.Add(item);
    }

    /// <summary>
    /// Removes an item from the inventory instance.
    /// </summary>
    /// <param name="itemData"> The ItemData to be removed. </param>
    public void RemoveItem(Item item)
    {
        _itemList.Remove(item);
    }

    /// <summary>
    /// Gets the _itemList of the inventory instance.
    /// </summary>
    /// <returns> The _itemList of the inventory instance. </returns>
    public List<Item> GetItemList()
    {
        return _itemList;
    }

    public Transform GetInventoryContainer()
    {
        return _inventoryContainer;
    }
}
