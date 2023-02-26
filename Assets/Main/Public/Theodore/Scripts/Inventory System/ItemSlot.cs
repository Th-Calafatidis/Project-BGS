// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A way to srore an Item instance on each item slot prefab, to be used whenever necessary from accessing each item slot.

using UnityEngine;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    // Store item data, item and inventory this item slot is part of
    // Note: These are accessed in InventoryManager.
    // I could use getters and setters instead of making them public, but i am running out of time so i decided to leave them as is.
    public ItemData ItemData;
    public Item Item;
    public Inventory Inventory;

    // Referances to inventory manager, player and shopManager
    private InventoryManager _inventoryManager;
    private GameObject _player;
    private GameObject _shopManager;

    private void Start()
    {
        _inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();

        _player = GameObject.Find("Player");
        _shopManager = GameObject.Find("ShopManager");
    }

    void Interact()
    {
        // If both the shop UI panel and the player inventory UI panels are active, player is trading
        if (UIManager.invUiActive && UIManager.shopUiActive)
        {
            // If the inventory the current item is in is the shop inventory,
            // we want to "buy" the item and transfer it to the player's inventory
            if (Inventory == ShopManager.Instance.Inventory)
            {
                // Check if the player has enough gold to buy the item
                if (_player.GetComponent<PlayerManager>().PlayerGoldAmount >= ItemData.Price)
                {
                    // Transfer the item
                    TransferToInventory(ShopManager.Instance.Inventory, PlayerManager.Instance.Inventory);

                    // Calculate the gold of the transaction
                    SubtractGoldAmount(ref _player.GetComponent<PlayerManager>().PlayerGoldAmount);
                    AddGoldAmount(ref _shopManager.GetComponent<ShopManager>().ShopGoldAmount);
                }

            }
            // // If the inventory the current item is in is the player's inventory,
            // we want to "sell" the item and transfer it to the shop's inventory
            else if (Inventory == PlayerManager.Instance.Inventory)
            {
                if (_shopManager.GetComponent<ShopManager>().ShopGoldAmount >= ItemData.Price)
                {
                    TransferToInventory(PlayerManager.Instance.Inventory, ShopManager.Instance.Inventory);

                    SubtractGoldAmount(ref _shopManager.GetComponent<ShopManager>().ShopGoldAmount);
                    AddGoldAmount(ref _player.GetComponent<PlayerManager>().PlayerGoldAmount);

                }
            }
        }

        // else we equip the item
        else if (UIManager.invUiActive && !UIManager.shopUiActive)
        {
            EquipItem();
        }

    }

    /// <summary>
    /// Transfers an item from an inventory to another.
    /// </summary>
    /// <param name="fromInventory"> The inventory to transfer the item from.</param>
    /// <param name="toInventory"> The inventory to transfer the item to. </param>
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

    /// <summary>
    /// Handles equipping an item.
    /// </summary>
    private void EquipItem()
    {
        // Check to see what ItemType is assigned to the item to know where to place it in the equip slots
        // and also which sprites to change for the visuals.
        // NOTE: I could use a switch statement to make it easier to read.
        // Also there is a lot of repeating code here. I could think about shortening it and using some functions and parameters to do the job cleaner.
        if (ItemData.ItemSlot == ItemData.ItemType.Head)
        {
            if (PlayerManager.Instance.HeadEquip != null)
            {
                PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.HeadEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());
            }

            // Equip to head
            PlayerManager.Instance.HeadFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.HeadBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.HeadLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.HeadRight.sprite = ItemData.SideSprite;

            // Remove from inventory and place on equipment slots
            PlayerManager.Instance.Inventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < PlayerManager.Instance.Inventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(PlayerManager.Instance.Inventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

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
                PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.WeaponEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

            }

            PlayerManager.Instance.WeaponFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.WeaponBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.WeaponLeft.sprite = ItemData.SideSprite;
            PlayerManager.Instance.WeaponRight.sprite = ItemData.SideSprite;

            // Remove from inventory and place on equipment slots
            PlayerManager.Instance.Inventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < PlayerManager.Instance.Inventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(PlayerManager.Instance.Inventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

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
                PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.ShieldEquip);

                // rerarrange inventory items
                _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

            }

            PlayerManager.Instance.ShieldFront.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldBack.sprite = ItemData.BackSprite;
            PlayerManager.Instance.ShieldLeft.sprite = ItemData.FrontSprite;
            PlayerManager.Instance.ShieldRight.sprite = ItemData.BackSprite;

            // Remove from inventory and place on equipment slots
            PlayerManager.Instance.Inventory.RemoveItem(Item);

            // Destroy inventory entries
            for (int i = 1; i < PlayerManager.Instance.Inventory.GetInventoryContainer().transform.childCount; i++)
            {
                Destroy(PlayerManager.Instance.Inventory.GetInventoryContainer().transform.GetChild(i).gameObject);
            }

            // rerarrange inventory items
            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

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
            PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.HeadEquip);

            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

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
            PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.WeaponEquip);

            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

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
            PlayerManager.Instance.Inventory.AddItem(PlayerManager.Instance.ShieldEquip);

            _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, PlayerManager.Instance.Inventory.GetInventoryContainer());

            // Remove item from equip slot
            PlayerManager.Instance.ShieldEquip = null;
        }

        // Destroy inventory entries
        DestroyInventoryEntries(PlayerManager.Instance.Inventory);

        // Setup the new entries
        _inventoryManager.SetInventory(PlayerManager.Instance.Inventory, DatabaseManager.Instance.InventoryContainer);

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

    void AddGoldAmount(ref int varToChange)
    {
        varToChange += ItemData.Price;
    }

    void SubtractGoldAmount(ref int varToChange)
    {
        varToChange -= ItemData.Price;
    }
}
