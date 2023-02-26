using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Transform ShopContainer;
    public Transform InventoryContainer;
    public Transform EquipmentContainer;
    public Transform HeadSlot;
    public Transform WeaponSlot;
    public Transform ShieldSlot;
}
