// Date: 2/26/2023
// Developer: Theodore Calafatidis
//
// Description: This class is used to store information that is going to be used globaly in many classes for ease of access to the data.

using TMPro;
using UnityEngine;

public class DatabaseManager : StaticInstance<DatabaseManager>
{
    #region UI Elements
    [Header("UI Elements")]
    [Tooltip("The player inventory UI panel.")]
    public Transform InventoryPanel;

    [Tooltip("The equiped items UI panel.")]
    public Transform EquipmentPanel;

    [Tooltip("The transform into which inventory item slot entries will be created.")]
    public Transform InventoryContainer;

    [Tooltip("The transform into which shop item slot entries will be created.")]
    public Transform ShopContainer;

    [Tooltip("The shop inventory UI panel.")]
    public Transform ShopPanel;

    [Tooltip("The player gold counter UI")]
    public TMP_Text PlayerGoldCounter;

    [Tooltip("The shop gold counter UI")]
    public TMP_Text ShopGoldCounter;
    #endregion

    #region Equipment UI Slots
    [Header("Equipment Slots")]
    [Tooltip("The transform of the equipment UI slot the head item icon is going to be placed on.")]
    public Transform HeadSlot;

    [Tooltip("The transform of the equipment UI slot the weapon item icon is going to be placed on.")]
    public Transform WeaponSlot;

    [Tooltip("The transform of the equipment UI slot the shield item icon is going to be placed on.")]
    public Transform ShieldSlot;
    #endregion

    public GameObject InventoryManager;

}
