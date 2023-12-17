using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TT.Utilities;


public class ShieldShopController : MonoBehaviour
{
    [SerializeField] private string type;

    QuickShopMultiDAL shopDAL;

    QuickShopItem[] items;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image icon;

    private void Awake()
    {
        shopDAL = new QuickShopMultiDAL();
        items = shopDAL.GetDAL(type).GetPrices();

        UpdateBtn();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            BuyItem();
        });
    }

    public void BuyItem()
    {

    }

    void UpdateBtn()
    {
        int curIndex = UserData.Instance.TurretShopLv;
        priceText.text = Utilities.MoneyToString(items[curIndex].Price);

        icon.sprite = Resources.Load<Sprite>("Sprites/Icons/" + (curIndex).ToString());
    }
}
