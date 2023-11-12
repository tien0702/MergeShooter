using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TT.Base;
using UnityEngine;

public class DropMergeObject : TTMonoBehaviour, IOnTouchEnded
{
    [SerializeField] LayerMask slotMask;

    ObjectMergeController mergeController;

    private void Awake()
    {
        mergeController = GetComponent<ObjectMergeController>();
    }

    public void OnTouchEnded()
    {
        var mergeObject = ObjectMergeController.GetMergeNearest(mergeController);
        if (mergeObject != null)
        {
            if (mergeController.CanMerge(mergeObject))
            {
                mergeController.Merge(mergeObject, null);
            }
            else
            {
                Transform newParent = mergeObject.transform.parent;
                mergeObject.transform.SetParent(transform.parent);
                transform.SetParent(newParent);

                this.MoveToLocalPosition(Vector3.zero);
                LeanTween.moveLocal(mergeObject.gameObject, Vector3.zero, this.time).setEase(this.leanTweenType);
            }
        }
        else
        {
            SlotMergeController slot = GetHitSlotMerge();
            if (slot == null)
            {
                this.MoveToLocalPosition(Vector3.zero);
            }
            else
            {
                transform.SetParent(slot.transform);

                this.MoveToLocalPosition(Vector3.zero);
            }
        }
    }

    SlotMergeController GetHitSlotMerge()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(touchPos.x, touchPos.y), Vector2.zero, Mathf.Infinity, slotMask.value);
        if (hit.transform == null) { return null; }

        return hit.transform.GetComponent<SlotMergeController>();
    }
}
