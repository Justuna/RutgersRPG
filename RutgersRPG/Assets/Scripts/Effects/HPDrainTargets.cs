using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPDrainTargets", menuName = "Effect/HP Drain from Targets")]
public class HPDrainTargets : Effect
{
    public int TargetHPChange;
    public float Multiplier;
    public bool Recoil;

    public override void Apply(Unit user, Unit target)
    {
        int drain = target.ChangeHealth(TargetHPChange);
        drain = (int)(drain * Multiplier);
        if (Recoil) drain *= -1;
        user.ChangeHealth(drain);

        if (TargetHPChange > 0) user.GiveHealing.Invoke();
        else user.DealDamage.Invoke();
    }
}
