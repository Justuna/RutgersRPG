using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatModifierSpec", menuName = "Modifier/Stat Modifier")]
public class StatModifier : Modifier
{
    public enum ModifierType { FLAT, ADD_PERCENT, MULTI_PERCENT };

    public float amount;
    public ModifierType modifierType;
    public Stat modifierStat;
}
