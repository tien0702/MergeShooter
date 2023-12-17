using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;
using System;
using System.IO;
using SimpleJSON;

public class UserData : Singleton<UserData>
{
    #region Observer
    public enum UserEvent { OnChangeCoin, OnChangeShield, OnChangeTurretShopLv };
    ObserverEvents<UserEvent, object> events = new ObserverEvents<UserEvent, object>();
    public ObserverEvents<UserEvent, object> Events => events;

    #endregion

    int coin;
    int shields;
    int waveIndex;
    int turretShopLv;

    public int Coin => coin;
    public int Shields
    {
        get { return shields; }
        set
        {
            this.shields = value;
            events.Notify(UserEvent.OnChangeShield, shields);
        }
    }
    public int WaveIndex
    {
        get { return waveIndex; }
        set { waveIndex = value; }
    }
    public int TurretShopLv
    {
        get { return turretShopLv; }
        set
        {
            turretShopLv = value;
            events.Notify(UserEvent.OnChangeTurretShopLv, this);
        }
    }

    public UserData()
    {
        string userDataPath = Application.streamingAssetsPath + "/user-data.json";
        if(!File.Exists(userDataPath)) 
        {
            string contents = Resources.Load<TextAsset>("Data/default-user-data").text;
            File.WriteAllText(userDataPath, contents);
        }
        string jsonData = File.ReadAllText(userDataPath);
        JSONNode data = JSONObject.Parse(jsonData);

        this.coin = data["coin"].AsInt;
        this.shields = data["shields"].AsInt;
        this.waveIndex = data["waveIndex"].AsInt;
        this.turretShopLv = data["turretShopLv"].AsInt;
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        events.Notify(UserEvent.OnChangeCoin, this);
    }

    public bool TakeCoin(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;
            events.Notify(UserEvent.OnChangeCoin, this);
            return true;
        }
        return false;
    }
}
