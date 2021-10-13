using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MoveType {WEAPON, MAGIC, STATUS, OTHER}

[CreateAssetMenu(fileName = "NewActionSpec", menuName = "Specification/Action")]
public class MoveSpec : ScriptableObject
{
    public string Name;
    public MoveType Type;
    public List<Effect> Effects;

    public void UseMove(Unit user, Unit target)
    {
        Debug.Log(user.Name + " used " + Name + " on " + target.Name);
        foreach (Effect e in Effects)
        {
            e.Apply(user, target);
        }
    }
}
