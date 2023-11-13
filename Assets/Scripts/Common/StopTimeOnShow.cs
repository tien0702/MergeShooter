using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimeOnShow : MonoBehaviour
{
    protected virtual void OnDisable()
    {
        Time.timeScale = 0;
    }

    protected virtual void OnEnable()
    {
        Time.timeScale = 1f;
    }

    protected virtual void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
