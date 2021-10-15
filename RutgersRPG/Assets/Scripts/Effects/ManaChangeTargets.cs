using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaChangeTargets", menuName = "Effect/Mana Change on Targets")]
public class ManaChangeTargets : Effect {
    public int RawManaChange;

    public override void Apply(Unit user, Unit target) {
        if (target is PCUnit) {
            PCUnit PCtarget = (PCUnit)target;
            PCtarget.SetMana(RawManaChange);
        }
    }
}
