using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierWrapper : ModifierWrapper
{
    StatModifier _modifier;

    public StatModifierWrapper(StatModifier modifier, Unit user, Unit target) : base(modifier.NumberOfTurns, user, target)
    {
        _modifier = modifier;
    }

    public StatModifier GetStatMod()
    {
        return _modifier;
    }
}
