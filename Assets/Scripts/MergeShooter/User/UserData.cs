using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;
using System;

public class UserData : Singleton<UserData>
{
    int coin;
    public int Coin => coin;
    List<Action> callbackOnChangeCoins = new List<Action>();

    void AddCallback(Action callback)
    {
        callbackOnChangeCoins.Add(callback);
    }

    void RemoveCallback(Action callback)
    {
        callbackOnChangeCoins.Remove(callback);
    }

    public UserData()
    {
    }

    public void AddCoin(int amoun)
    {

    }

    public void GetCoin(int amount)
    {

    }

}
