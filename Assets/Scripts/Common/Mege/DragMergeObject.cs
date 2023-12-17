using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DragMergeObject : MonoBehaviour, IOnTouchBegan, IOnTouchMoved, IOnTouchEnded
{
    Vector2 offset;

    SortingGroup sortingGroup;

    int sortingOrder;

    private void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        sortingOrder = sortingGroup.sortingOrder;
    }

    public void OnTouchBegan()
    {
        sortingGroup.sortingOrder = 99;
        offset = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position;
    }

    public void OnTouchMoved()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - offset;
    }

    public void OnTouchEnded()
    {
        sortingGroup.sortingOrder = sortingOrder;
    }
}
