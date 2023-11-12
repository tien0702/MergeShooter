using System.Collections;
using System.Collections.Generic;
using TT.Entity;
using UnityEngine;

public interface IHit
{
    void TakeHit(EntityController attacker);
}
