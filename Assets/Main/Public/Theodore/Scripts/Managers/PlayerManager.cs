// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A class holding data relative to the player.

using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : StaticInstance<PlayerManager>
{
    // The player's inventory
    private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } set { _inventory = value; } }

    private InventoryManager _inventoryManager;

    public int PlayerGoldAmount;

    // Equipment Sprite Parts
    // The SpriteRenderers whose sprites will need to be changed when the player equips / unequips an item
    [Header("Head Sprite Parts")]
    public SpriteRenderer HeadFront;
    public SpriteRenderer HeadBack;
    public SpriteRenderer HeadLeft;
    public SpriteRenderer HeadRight;

    [Header("Weapon Parts")]
    public SpriteRenderer WeaponFront;
    public SpriteRenderer WeaponBack;
    public SpriteRenderer WeaponLeft;
    public SpriteRenderer WeaponRight;

    [Header("Shield Parts")]
    public SpriteRenderer ShieldFront;
    public SpriteRenderer ShieldBack;
    public SpriteRenderer ShieldLeft;
    public SpriteRenderer ShieldRight;

    // Equipped Items
    public Item HeadEquip;
    public Item WeaponEquip;
    public Item ShieldEquip;

    #region Debugging

    // A list to place items from the inspector on the player to be loaded in the player's inventory
    public List<ItemData> ItemList;

    // Method that loads the items from the list in the inventory
    public void LoadItems()
    {
        foreach (ItemData itemData in ItemList)
        {
            _inventory.AddItem(new Item(itemData));
        }
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        // Create a new inventory instance for the player when game starts
        _inventory = new Inventory(DatabaseManager.Instance.InventoryContainer);

        // Load the items the player currently has in his inventory
        // Some preset items are placed in his inventory for testing / showcase purposes
        LoadItems();

        _inventoryManager = DatabaseManager.Instance.InventoryManager.GetComponent<InventoryManager>();

        _inventoryManager.SetInventory(_inventory, _inventory.GetInventoryContainer());

    }    
}
