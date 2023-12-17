using System.Collections;
using System.Collections.Generic;
using TMPro;
using TT.DesignPattern;
using UnityEngine;

public class ShieldController : SingletonBehaviour<ShieldController>
{
    #region Observer

    public enum ShieldEventType { OnAddShield, OnBreakShield }
    protected ObserverEvents<ShieldEventType, int> shieldEvent = new ObserverEvents<ShieldEventType, int>();
    public ObserverEvents<ShieldEventType, int> ShieldEvent => shieldEvent;

    #endregion
    
    [SerializeField] private TextMeshProUGUI shieldInfo;

    public int maxShield = 5;

    private void Start()
    {
        shieldInfo.text = UserData.Instance.Shields.ToString();
    }

    public void AddShield(int amount)
    {
        UserData.Instance.Shields += 1;
        shieldInfo.text = UserData.Instance.Shields.ToString();
        shieldEvent.Notify(ShieldEventType.OnAddShield, amount);
    }

    public void BreakShield()
    {
        UserData.Instance.Shields -= 1;
        shieldInfo.text = UserData.Instance.Shields.ToString();
        shieldEvent.Notify(ShieldEventType.OnBreakShield, UserData.Instance.Shields);
    }
}
