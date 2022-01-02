using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddStatModifier", menuName = "Effect/Add Stat Modifier")]
public class AddStatModifier : Effect
{
    public StatModifier Modifier;

    public override void Apply(Unit user, Unit target)
    {
        ModifierWrapper m = new StatModifierWrapper(Modifier, user, target);
        target.AddModifier(m);
    }
}
