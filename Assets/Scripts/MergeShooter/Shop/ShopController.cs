using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopSlotController slotPrefab;

    private void Awake()
    {
        
    }

    public void BuyTurret()
    {
        if(UserData.Instance.TakeCoin(0))
        {
            GameAreaController.Instance.AddTurret();
        }
    }
}
