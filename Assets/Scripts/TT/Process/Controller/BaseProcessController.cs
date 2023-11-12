using UnityEngine;
using TT.Process.Base;

namespace TT.Process.Controller
{
    public abstract class BaseProcessController : BaseProcess
    {
        protected virtual void Display()
        {
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentValue / maxValue, currentScale.y, currentScale.z);
        }
    }
}
