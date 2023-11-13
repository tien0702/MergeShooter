using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;

public class FloatingTextController : SingletonBehaviour<FloatingTextController>
{
    protected ObjectPooler pooler;

    private void Start()
    {

    }

    public FloatingText GetFloatingText()
    {
        FloatingText text = pooler.GetObject().GetComponent<FloatingText>();
        text.gameObject.SetActive(true);
        return text;
    }

    public void SpawnText(Vector3 pos, string text)
    {
        FloatingText fText = this.GetFloatingText();
        fText.transform.position = pos;
        fText.SetText(text);
    }
}
