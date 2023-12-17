using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TT.Utilities;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ShopSlotController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI coinTxt;
    [SerializeField] private Image iconBG;
    [SerializeField] private Image iconIMG;

    ItemShopInfo itemShop;
    public ItemShopInfo ItemShop => itemShop;

    public virtual void Init(ItemShopInfo item)
    {
        this.itemShop = item;

        coinTxt.text = itemShop.Price.ToString();
        itemDescription.text = itemShop.Description;
    }

    public virtual void BuyItem()
    {
        if(UserData.Instance.TakeCoin(itemShop.Price))
        {

        }
        else
        {

        }
    }
}
