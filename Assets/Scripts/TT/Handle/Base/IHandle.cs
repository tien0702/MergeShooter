using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandle
{
    void Handle();
    Action<IHandle> OnEndHandle { set; }
}
