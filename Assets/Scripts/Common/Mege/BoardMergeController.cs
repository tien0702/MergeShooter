using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardMergeController : MonoBehaviour
{
    [SerializeField] protected string layerName;
    [SerializeField] string boardType;
    public string BoardType => boardType;
    List<SlotMergeController> slots = new List<SlotMergeController>();

    protected virtual void Awake()
    {
        int layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in transform)
        {
            if (layer != -1) child.gameObject.layer = layer;
            var slot = child.AddComponent<SlotMergeController>();
            slots.Add(slot);
            BoxCollider2D collider = child.AddComponent<BoxCollider2D>();
            collider.size = Vector2.one * 2 * MergeController.DistanceMerge;
        }
    }

    public SlotMergeController[] GetEmptySlots()
    {
        return slots.Where(slot => slot.ObjInSlot == null).ToArray();
    }
}
