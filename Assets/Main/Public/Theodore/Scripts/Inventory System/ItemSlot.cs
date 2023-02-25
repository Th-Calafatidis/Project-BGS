// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A way to srore an Item instance on each item slot prefab, to be used whenever necessary from accessing each item slot.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public ItemData ItemData;
    public Item Item;
    public Inventory Inventory;

    private Inventory _playerInventory;
    private Inventory _shopInventory;

    private InventoryManager _inventoryManager;

    private Transform _inventoryContainer;
    private Transform _shopContainer;

    private void Start()
    {
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerManager>().Inventory;
        _shopInventory = GameObject.Find("ShopManager").GetComponent<ShopManager>().Inventory;

        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        _inventoryContainer = GameObject.Find("InventorySlotContainer").transform;
        _shopContainer = GameObject.Find("ShopSlotContainer").transform;
    }

    //public void Interact()
    //{
    //    // add to inventory
    //    _playerInventory.AddItem(Item);

    //    // destroy inventory entries
    //    for (int i = 1; i < _inventoryContainer.transform.childCount; i++)
    //    {
    //        Destroy(_inventoryContainer.transform.GetChild(i).gameObject);
    //    }

    //    // rerarrange inventory items
    //    _inventoryManager.SetInventory(_playerInventory, _inventoryContainer);

    //    // remove the item from itemlist of the container - this does not remove the item.
    //    _shopInventory.RemoveItem(Item);

    //    // destroy the rest of container the entries
    //    for (int i = 1; i < _shopContainer.transform.childCount; i++)
    //    {
    //        Destroy(_shopContainer.transform.GetChild(i).gameObject);
    //    }

    //    // rearrange container items
    //    _inventoryManager.SetInventory(_shopInventory, _shopContainer);

    //}

    void TransferToInventory(Inventory fromInventory, Inventory toInventory)
    {
        // add to inventory
        toInventory.AddItem(Item);

        // destroy inventory entries
        for (int i = 1; i < toInventory.GetInventoryContainer().transform.childCount; i++)
        {
            Destroy(toInventory.GetInventoryContainer().transform.GetChild(i).gameObject);
        }

        // rerarrange inventory items
        _inventoryManager.SetInventory(toInventory, toInventory.GetInventoryContainer());

        // remove the item from itemlist of the container - this does not remove the item.
        fromInventory.RemoveItem(Item);

        // destroy the rest of container the entries
        for (int i = 1; i < fromInventory.GetInventoryContainer().transform.childCount; i++)
        {
            Destroy(fromInventory.GetInventoryContainer().transform.GetChild(i).gameObject);
        }

        // rearrange container items
        _inventoryManager.SetInventory(fromInventory, fromInventory.GetInventoryContainer());
    }

    void Interact()
    {
        if (Inventory == _shopInventory)
        {
            TransferToInventory(_shopInventory, _playerInventory);
        }
        else if (Inventory == _playerInventory)
        {
            TransferToInventory(_playerInventory, _shopInventory);
        }
    }
}
