using System.Collections;
using System.Collections.Generic;
using TMPro;
using TT.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class TurretShopController : QuickShopController
{

    QuickShopMultiDAL shopDAL;

    QuickShopItem[] items;

    [SerializeField] private TextMeshProUGUI priceText, lvText;
    [SerializeField] private Image turretIcon;

    protected override void Awake()
    {
        base.Awake();
        shopDAL = new QuickShopMultiDAL();
        items = shopDAL.GetDAL(itemType).GetPrices();

        UpdateBtn();
    }

    void UpdateBtn()
    {
        int curIndex = UserData.Instance.TurretShopLv;
        priceText.text = Utilities.MoneyToString(items[curIndex].Price);
        lvText.text = (curIndex).ToString();

        turretIcon.sprite = Resources.Load<Sprite>("Sprites/Icons/" + (curIndex).ToString());
    }

    protected override void Buy()
    {
        if (GameAreaController.Instance.GetBoardByType("QueueType").GetEmptySlots() == null)
        {
            PopupTextNotify.Instance.PopupNotify("Not enough slot!", Color.red);
            return;
        }

        int current = UserData.Instance.TurretShopLv;
        if (!UserData.Instance.TakeCoin(items[current].Price))
        {
            PopupTextNotify.Instance.PopupNotify("Not enough coin!", Color.red);
            return;
        }
        UserData.Instance.TurretShopLv += 1;
        GameAreaController.Instance.AddTurret(new TT.Entity.EntityInfo("turret", UserData.Instance.TurretShopLv), "QueueType");

        UpdateBtn();
    }
}
