using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    // A preset list of items the shop will hold. Can be set from the inspector.
    public List<ItemData> ItemList;

    // The inventory instance the shop will have.
    private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } set { _inventory = value; } }

    private InventoryManager _inventoryManager;

    private Transform _inventoryContainer;

    public int goldAmount;

    private void Awake()
    {
        Instance = this;

        _inventoryContainer = GameObject.Find("ShopSlotContainer").transform;

        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        // Create inventory instance for the shop
        _inventory = new Inventory(_inventoryContainer);

        // Load the preset items into the inventory instance
        LoadItems(ItemList);

        // Create the UI for the shop inventory
        _inventoryManager.SetInventory(_inventory, _inventory.GetInventoryContainer());
    }

    private void LoadItems(List<ItemData> itemDataList)
    {
        foreach (ItemData itemData in itemDataList)
        {
            _inventory.AddItem(new Item(itemData));
        }
    }

}
