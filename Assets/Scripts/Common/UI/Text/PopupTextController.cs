using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;
using TMPro;
using TT;

public class PopupTextController : TTMonoBehaviour
{
    [SerializeField] private Vector3 TargetPos;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(string message, Color color)
    {
        textMeshProUGUI.text = message;
        textMeshProUGUI.color = color;
    }

    private void OnEnable()
    {
        this.MoveToLocalPosition(TargetPos);
        LeanTween.alphaCanvas(canvasGroup, 0, time).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
