using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text playerGoldCounter;
    [SerializeField] private TMP_Text shopGoldCounter;

    private void Update()
    {
        playerGoldCounter.text = PlayerManager.Instance.goldAmount.ToString() + " GP";
        shopGoldCounter.text = ShopManager.Instance.goldAmount.ToString() + " GP";
    }
}
