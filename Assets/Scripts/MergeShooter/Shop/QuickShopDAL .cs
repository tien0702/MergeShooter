using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DAL;

public class QuickShopDAL : SingleDAL
{
    public QuickShopItem GetPrice(int index)
    {
        return this.GetData<QuickShopItem>(index);
    }

    public QuickShopItem[] GetPrices()
    {
        return this.GetDatas<QuickShopItem>();
    }
}
