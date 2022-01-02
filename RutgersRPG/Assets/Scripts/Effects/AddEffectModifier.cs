using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddEffectModifier", menuName = "Effect/Add Effect Modifier")]
public class AddEffectModifier : Effect
{
    public EffectModifier Modifier;

    public override void Apply(Unit user, Unit target)
    {
        ModifierWrapper m = new EffectModifierWrapper(Modifier, user, target);
        target.AddModifier(m);
    }
}
