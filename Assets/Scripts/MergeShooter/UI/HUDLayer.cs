using System.Collections;
using System.Collections.Generic;
using TMPro;
using TT.Utilities;
using UnityEngine;

public class HUDLayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;


    private void Start()
    {
        UserData.Instance.Events.RegisterEvent(UserData.UserEvent.OnChangeCoin, OnChangeCoin);


        coinTxt.text = Utilities.MoneyToString(UserData.Instance.Coin);
    }

    private void OnDestroy()
    {
        UserData.Instance.Events.UnRegisterEvent(UserData.UserEvent.OnChangeCoin, OnChangeCoin);
    }

    void OnChangeCoin(object data)
    {
        coinTxt.text = Utilities.MoneyToString(UserData.Instance.Coin);
    }
}
