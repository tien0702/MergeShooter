using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardMergeController : MonoBehaviour
{
    [SerializeField] protected string layerName;
    string boardType;
    List<SlotMergeController> slots = new List<SlotMergeController>();

    protected virtual void Awake()
    {
        int layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in transform)
        {
            if (layer != -1) child.gameObject.layer = layer;
            slots.Add(child.AddComponent<SlotMergeController>());
            CircleCollider2D collider = child.AddComponent<CircleCollider2D>();
            collider.radius = MergeObjectController.DistanceMerge;
        }
    }

    public SlotMergeController[] GetEmptySlots()
    {
        return slots.Where(slot => slot.ObjInSlot == null).ToArray();
    }
}
