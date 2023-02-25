// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A way to srore an Item instance on each item slot prefab, to be used whenever necessary from accessing each item slot.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// Needs heavy refactoring!!!
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

    public GameObject Player;
    public GameObject Shop;

    public GameObject playerHead;

    private void Start()
    {
        playerHead = GameObject.Find("Helmet");

        _playerInventory = GameObject.Find("Player").GetComponent<PlayerManager>().Inventory;
        _shopInventory = GameObject.Find("ShopManager").GetComponent<ShopManager>().Inventory;

        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        _inventoryContainer = GameObject.Find("InventorySlotContainer").transform;
        _shopContainer = GameObject.Find("ShopSlotContainer").transform;

        Player = GameObject.Find("Player");
        Shop = GameObject.Find("ShopManager");

    }

    void Interact()
    {
        if (ToggleUI.invUiActive && ToggleUI.shopUiActive)
        {
            if (Inventory == _shopInventory)
            {
                if (Player.GetComponent<PlayerManager>().goldAmount >= ItemData.Price)
                {
                    TransferToInventory(_shopInventory, _playerInventory);

                    Player.GetComponent<PlayerManager>().goldAmount -= ItemData.Price;
                    Shop.GetComponent<ShopManager>().goldAmount += ItemData.Price;
                }

            }
            else if (Inventory == _playerInventory)
            {
                if (Shop.GetComponent<ShopManager>().goldAmount >= ItemData.Price)
                {
                    TransferToInventory(_playerInventory, _shopInventory);

                    Shop.GetComponent<ShopManager>().goldAmount -= ItemData.Price;
                    Player.GetComponent<PlayerManager>().goldAmount += ItemData.Price;
                }
            }
        }

        // else use item
        else if (ToggleUI.invUiActive && !ToggleUI.shopUiActive)
        {
            ChangeVisuals();
        }

    }

    private void TransferToInventory(Inventory fromInventory, Inventory toInventory)
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

    private void ChangeVisuals()
    {
        if (ItemData.ItemSlot == ItemData.ItemType.Head)
        {
            // Equip to head
            PlayerManager.Instance.HeadFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.HeadBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.HeadLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.HeadRight.sprite = ItemData.SideSprite;

            // Remove from inventory and put it into equip slot
        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Weapon)
        {
            PlayerManager.Instance.WeaponFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.WeaponBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.WeaponLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.WeaponRight.sprite = ItemData.SideSprite;
        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Shield)
        {
            PlayerManager.Instance.ShieldFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.ShieldLeft.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldRight.sprite = ItemData.BackSprite;
        }
    }
}
