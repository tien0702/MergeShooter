using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;

public class PopupTextNotify : SingletonBehaviour<PopupTextNotify>
{
    [SerializeField] private PopupTextController PopupPrefab;

    protected override void Awake()
    {
        base.Awake();
        PopupPrefab = Resources.Load<PopupTextController>("Prefabs/UI/PopupText");
    }

    public void PopupNotify(string text, Color color)
    {
        var popup = Instantiate(PopupPrefab, transform);
        popup.transform.localPosition = Vector3.zero;
        popup.Init(text, color);
    }
}
