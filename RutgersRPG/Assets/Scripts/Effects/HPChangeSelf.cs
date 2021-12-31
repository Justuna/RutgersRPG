using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPChangeSelf", menuName = "Effect/HP Change on Self")]
public class HPChangeSelf : Effect
{
    public int RawHPChange;

    public override void Apply(Unit user, Unit target)
    {
        user.ChangeHealth(RawHPChange);
        if (RawHPChange > 0) user.GiveHealing.Invoke();
        else user.DealDamage.Invoke();
    }
}
