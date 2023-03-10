using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : StaticInstance<ShopManager>
{
    // A preset list of items the shop will hold. Can be set from the inspector.
    public List<ItemData> ItemList;

    // The inventory instance the shop will have.
    private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } set { _inventory = value; } }

    private InventoryManager _inventoryManager;

    public int ShopGoldAmount;

    protected override void Awake()
    {
        base.Awake();

        _inventoryManager = DatabaseManager.Instance.InventoryManager.GetComponent<InventoryManager>();

        // Create inventory instance for the shop
        _inventory = new Inventory(DatabaseManager.Instance.ShopContainer);

        // Load the preset items into the inventory instance
        LoadItems(ItemList);

        // Create the UI for the shop inventory
        _inventoryManager.SetInventory(_inventory, _inventory.GetInventoryContainer());
    }

    // Loads the items placed in the itemDataList into the inventory -- Can take an inventory as param to be used for any inventory
    private void LoadItems(List<ItemData> itemDataList)
    {
        foreach (ItemData itemData in itemDataList)
        {
            _inventory.AddItem(new Item(itemData));
        }
    }

}
