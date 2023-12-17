using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuickShopItem
{
    public int Price;
}

public abstract class QuickShopController : MonoBehaviour
{
    [SerializeField] protected string itemType;
    public string ItemType => itemType;

    protected Button button;


    protected virtual void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => { Buy(); });
    }

    protected abstract void Buy();
}
