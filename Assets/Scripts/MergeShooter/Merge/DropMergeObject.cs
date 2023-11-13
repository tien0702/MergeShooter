using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TT.Base;
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
        /*      var canMergeObject = MergeController.GetMergeNearest(mergeController);
                var curSlot = GetComponentInParent<SlotMergeController>();
                if (canMergeObject != null)
                {
                    var newSlot = canMergeObject.GetComponentInParent<SlotMergeController>();
                    if (mergeController.CanMerge(canMergeObject))
                    {
                        Debug.Log("merge");
                        curSlot.TakeOut();
                        var mergeObj = newSlot.TakeOut();
                        Debug.Log(newSlot.PutIn(mergeController));

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
                        Debug.Log("swap");
                        curSlot.TakeOut();
                        newSlot.TakeOut();

                        Debug.Log(newSlot.PutIn(mergeController));
                        Debug.Log(curSlot.PutIn(canMergeObject));

                        this.MoveToLocalPosition(Vector3.zero);
                        LeanTween.moveLocal(canMergeObject.gameObject, Vector3.zero, this.time).setEase(this.leanTweenType);
                    }
                }
                else
                {
                    SlotMergeController slot = GetHitSlotMerge();

                    if (slot == null || slot.transform.Equals(curSlot.transform))
                    {
                        Debug.Log("back");
                        this.MoveToLocalPosition(Vector3.zero);
                    }
                    else
                    {
                        Debug.Log("chage slot");
                        curSlot.TakeOut();
                        Debug.Log(slot.PutIn(mergeController));

                        this.MoveToLocalPosition(Vector3.zero);
                    }
                }*/

        // -----------------------------------


        SlotMergeController hitSlot = GetHitSlotMerge();
        if (hitSlot != null)
        {
            var curSlot = GetComponentInParent<SlotMergeController>();
            if (hitSlot.ObjInSlot != null)
            {
                if (mergeController.CanMerge(hitSlot.ObjInSlot))
                {
                    curSlot.TakeOut();
                    var mergeObj = hitSlot.TakeOut();
                    Debug.Log(hitSlot.PutIn(mergeController));

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
                    Debug.Log("swap");
                    curSlot.TakeOut();
                    MergeController mergeObj = hitSlot.TakeOut();

                    Debug.Log(hitSlot.PutIn(mergeController));
                    Debug.Log(curSlot.PutIn(mergeObj));

                    this.MoveToLocalPosition(Vector3.zero);
                    LeanTween.moveLocal(mergeObj.gameObject, Vector3.zero, this.time).setEase(this.leanTweenType);
                }
            }
            else
            {
                Debug.Log("change");
                curSlot.TakeOut();
                Debug.Log(hitSlot.PutIn(mergeController));

                this.MoveToLocalPosition(Vector3.zero);
            }
        }
        else
        {
            Debug.Log("back");
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
