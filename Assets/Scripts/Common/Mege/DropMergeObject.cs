using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TT;
using UnityEngine;

public class DropMergeObject : TTMonoBehaviour, IOnTouchEnded
{
    [SerializeField] LayerMask slotMask;
    [SerializeField] GameObject mergeFx;

    MergeController mergeController;

    private void Awake()
    {
        mergeController = GetComponent<MergeController>();
    }

    public void OnTouchEnded()
    {
        SlotMergeController hitSlot = GetHitSlotMerge();
        if (hitSlot != null && hitSlot.transform.Equals(transform.parent))
        {
            this.MoveToLocalPosition(Vector3.zero);
        }
        else if (hitSlot != null)
        {
            var curSlot = GetComponentInParent<SlotMergeController>();
            if (hitSlot.ObjInSlot != null)
            {
                if (mergeController.CanMerge(hitSlot.ObjInSlot))
                {
                    curSlot.TakeOut();
                    var mergeObj = hitSlot.TakeOut();
                    hitSlot.PutIn(mergeController);
                    this.MoveToLocalPosition(Vector3.zero, () =>
                    {
                        mergeController.Merge(mergeObj, null);
                        Destroy(mergeObj.gameObject);
                        var fx = Instantiate(mergeFx, transform);
                        fx.transform.localPosition = Vector3.zero;
                    });
                }
                else
                {
                    curSlot.TakeOut();
                    MergeController mergeObj = hitSlot.TakeOut();

                    hitSlot.PutIn(mergeController);
                    curSlot.PutIn(mergeObj);

                    this.MoveToLocalPosition(Vector3.zero);
                    LeanTween.moveLocal(mergeObj.gameObject, Vector3.zero, this.time).setEase(this.leanTweenType);
                }
            }
            else
            {
                curSlot.TakeOut();
                hitSlot.PutIn(mergeController);

                this.MoveToLocalPosition(Vector3.zero);
            }
        }
        else
        {
            this.MoveToLocalPosition(Vector3.zero);
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
