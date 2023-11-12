using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    bool IsSuitable { get; }
    Action<ICondition> OnSuitable { set; }
}
