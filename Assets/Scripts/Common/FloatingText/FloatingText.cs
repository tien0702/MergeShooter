using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using TT.DesignPattern;
using UnityEngine.Pool;
using TMPro;

public class FloatingText : TTMonoBehaviour
{
    [SerializeField] protected TextMeshPro text;
    [SerializeField] protected float scaleRate;
    [SerializeField] protected float xRate;
    [SerializeField] protected float yRate;

    Vector3 originScale;

    protected void Awake()
    {
        text = GetComponent<TextMeshPro>();
        originScale = transform.localScale;
    }

    public void SetText(string content)
    {
        text.text = content;
        Vector2 scale = originScale * Random.Range(1f, scaleRate);
        Vector2 position = new Vector2(transform.position.x + Random.Range(-xRate, xRate), transform.position.y + Random.Range(0f, yRate));
        this.ScalceTo(scale, OnEndFloat);
        this.MoveToPosition(position);
    }

    protected void OnEndFloat()
    {
        this.gameObject.SetActive(false);
    }
}
