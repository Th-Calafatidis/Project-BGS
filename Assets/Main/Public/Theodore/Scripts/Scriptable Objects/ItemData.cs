// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: A scriptable object template that will facilitate different item object creation. Each will hold all the data relevant to that item.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Head,
        Body,
        Weapon,
        Shield
    }

    public string ItemName;

    public ItemType ItemSlot;

    public Sprite ItemIcon;

    public int Price;
}
