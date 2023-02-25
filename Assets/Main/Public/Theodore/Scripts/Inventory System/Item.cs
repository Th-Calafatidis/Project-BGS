// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: An item base class for the creation of new items. Each item instance will hold data loaded to it from the respective ItemData it was created from.

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Head,
        Body,
        Weapon,
        Shield
    }

    public ItemData ItemData;
    public Sprite ItemIcon;
    public int Price;
    public ItemType ItemSlot;

    /// <summary>
    /// A constructor used to create a new Item instrance.
    /// </summary>
    /// <param name="itemData"> The ItemData this item instance will hold. </param>
    public Item(ItemData itemData)
    {
        ItemData = itemData;
        ItemIcon = itemData.ItemIcon;
        Price = itemData.Price;
        ItemSlot = (ItemType)itemData.ItemSlot;
    }

    /// <summary>
    /// Method used to get the icon sprite of an item.
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite()
    {
        return ItemData.ItemIcon;
    }
}
