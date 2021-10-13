using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPChangeTargets", menuName = "Effect/HP Change on Targets")]
public class HPChangeTargets : Effect
{
    public int RawHPChange;

    public override void Apply(Unit user, Unit target)
    {
        target.SetHealth(RawHPChange);
    }
}
