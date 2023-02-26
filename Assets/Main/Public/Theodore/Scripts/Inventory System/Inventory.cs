// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: The base class from which all inventory instances will be created.

using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // A list of Items that the inventory instance will hold
    private List<Item> _itemList;

    // The transform into which the inventory will be displayed
    private Transform _inventoryContainer;

    /// <summary>
    /// A constructor used to create a new inventory instance.
    /// </summary>
    public Inventory(Transform inventoryContainer)
    {
        // Initialise a new Item list for this inventory instance to use
        _itemList = new List<Item>();

        // Set the container into which the inventory will be created
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

    /// <summary>
    /// Gets the transform of the inventory container the items are displayed in
    /// </summary>
    /// <returns> The transform of the inventory container </returns>
    public Transform GetInventoryContainer()
    {
        return _inventoryContainer;
    }
}
