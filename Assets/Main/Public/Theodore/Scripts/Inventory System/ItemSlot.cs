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
        _shopContainer = DatabaseManager.Instance.ShopContainer;

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

        // remove the item from itemlist of the container
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
        // This duplicates the item!!!
        

        if (ItemData.ItemSlot == ItemData.ItemType.Head)
        {

            if (PlayerManager.Instance.HeadEquip != null)
            {
                _playerInventory.AddItem(PlayerManager.Instance.HeadEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            }

            // Equip to head
            PlayerManager.Instance.HeadFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.HeadBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.HeadLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.HeadRight.sprite = ItemData.SideSprite;

            // Remove from inventory and place on equipment slots
            _playerInventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < _playerInventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(_playerInventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Equip to slot
            PlayerManager.Instance.HeadEquip = Item;

            // Place Icon to Slot
            DatabaseManager.Instance.HeadSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = Item.ItemIcon;

            // Store ItemData
            DatabaseManager.Instance.HeadSlot.GetComponent<ItemSlot>().ItemData = Item.ItemData;

        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Weapon)
        {

            if (PlayerManager.Instance.WeaponEquip != null)
            {
                _playerInventory.AddItem(PlayerManager.Instance.WeaponEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            }

            PlayerManager.Instance.WeaponFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.WeaponBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.WeaponLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.WeaponRight.sprite = ItemData.SideSprite;

            // Remove from inventory and place on equipment slots
            _playerInventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < _playerInventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(_playerInventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Equip to slot
            PlayerManager.Instance.WeaponEquip = Item;

            // Place Icon to Slot
            DatabaseManager.Instance.WeaponSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = Item.ItemIcon;

            // Store ItemData
            DatabaseManager.Instance.WeaponSlot.GetComponent<ItemSlot>().ItemData = Item.ItemData;

        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Shield)
        {
            if (PlayerManager.Instance.ShieldEquip != null)
            {
                _playerInventory.AddItem(PlayerManager.Instance.ShieldEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            }

            PlayerManager.Instance.ShieldFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.ShieldLeft.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldRight.sprite = ItemData.BackSprite;

            // Remove from inventory and place on equipment slots
            _playerInventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < _playerInventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(_playerInventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Equip to slot
            PlayerManager.Instance.ShieldEquip = Item;

            // Place Icon to Slot
            DatabaseManager.Instance.ShieldSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = Item.ItemIcon;

            // Store ItemData
            DatabaseManager.Instance.ShieldSlot.GetComponent<ItemSlot>().ItemData = Item.ItemData;
        }
    }

    public void RemoveEquipment()
    {
        if (ItemData == null)
            return;

        // Load Icon
        Sprite icon = Resources.Load<Sprite>("IconEmpty");

        if (ItemData.ItemSlot == ItemData.ItemType.Head)
        {
            // Change icon Sprites to null
            DatabaseManager.Instance.HeadSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = icon;

            // Change visuals to null
            PlayerManager.Instance.HeadFront.sprite = null;
            PlayerManager.Instance.HeadBack.sprite = null;
            PlayerManager.Instance.HeadLeft.sprite = null;
            PlayerManager.Instance.HeadRight.sprite = null;

            // Add item to inventory
            _playerInventory.AddItem(PlayerManager.Instance.HeadEquip);

            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Remove item from equip slot
            PlayerManager.Instance.HeadEquip = null;
        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Weapon)
        {
            // Change icon Sprites to null
            DatabaseManager.Instance.WeaponSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = icon;

            // Change visuals to null
            PlayerManager.Instance.WeaponFront.sprite = null;
            PlayerManager.Instance.WeaponBack.sprite = null;
            PlayerManager.Instance.WeaponLeft.sprite = null;
            PlayerManager.Instance.WeaponRight.sprite = null;

            // Add item to inventory
            _playerInventory.AddItem(PlayerManager.Instance.WeaponEquip);

            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Remove item from equip slot
            PlayerManager.Instance.WeaponEquip = null;
        }
        else if (ItemData.ItemSlot == ItemData.ItemType.Shield)
        {
            // Change icon Sprites to null
            DatabaseManager.Instance.ShieldSlot.GetComponentInChildren<Icon>().gameObject.GetComponent<Image>().sprite = icon;

            // Change visuals to null
            PlayerManager.Instance.ShieldFront.sprite = null;
            PlayerManager.Instance.ShieldBack.sprite = null;
            PlayerManager.Instance.ShieldLeft.sprite = null;
            PlayerManager.Instance.ShieldRight.sprite = null;

            // Add item to inventory
            _playerInventory.AddItem(PlayerManager.Instance.ShieldEquip);

            _inventoryManager.SetInventory(_playerInventory, _playerInventory.GetInventoryContainer());

            // Remove item from equip slot
            PlayerManager.Instance.ShieldEquip = null;
        }

        // Destroy inventory entries
        DestroyInventoryEntries(_playerInventory);

        // Setup the new entries
        _inventoryManager.SetInventory(_playerInventory, DatabaseManager.Instance.InventoryContainer);

        ItemData = null;
    }

    void DestroyInventoryEntries(Inventory inventory)
    {
        // Destroy inventory entries
        for (int i = 1; i < inventory.GetInventoryContainer().transform.childCount; i++)
        {
            Destroy(inventory.GetInventoryContainer().transform.GetChild(i).gameObject);
        }
    }
}
