using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("wall");
        if (collision.gameObject.layer != layerMask.value) { return; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.layer != layerMask.value) { return; }
    }
}
