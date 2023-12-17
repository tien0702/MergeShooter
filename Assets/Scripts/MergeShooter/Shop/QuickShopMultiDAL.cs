using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DAL;

public class QuickShopMultiDAL : MultiDAL
{
    public QuickShopMultiDAL()
    {
        this.LoadData<QuickShopDAL>("QuickShopItem");
    }

    public QuickShopDAL GetDAL(string type)
    {
        return this.GetBaseSingleVO(type) as QuickShopDAL;
    }
}
