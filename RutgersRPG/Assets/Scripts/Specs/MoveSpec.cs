using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewActionSpec", menuName = "Specification/Action")]
public class MoveSpec : ScriptableObject
{
    public string Name;
    public int ManaCost;
    public int Priority;
    public List<Effect> Effects;
    public TargetSpec Targets;

    public bool CheckCost(Unit user) 
    {
        if (user is PCUnit) {
            PCUnit PCuser = (PCUnit)user;
            return PCuser.GetMana() >= ManaCost;
        }
        return true;
    }

    public int GetCost() 
    {
        return ManaCost;
    }

    public void UseMove(Unit user, List<Unit> targets)
    {
        foreach (Unit t in targets) {
            Debug.Log(user.Name + " used " + Name + " on " + t.Name);
            foreach (Effect e in Effects) {
                e.Apply(user, t);
            }
        }
    }
}
