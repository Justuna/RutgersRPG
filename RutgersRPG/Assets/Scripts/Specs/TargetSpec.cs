using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTargetSpec", menuName = "Specification/Targets")]
public class TargetSpec : ScriptableObject
{
    public bool ChooseTargets;
    public int NumberOfTargets;

    public bool TargetTeam;
    public bool Friendly;
    
    public Predicate<Unit> ConstructPredicate(TeamType team) 
    {
        if (TargetTeam) {
            if (Friendly) return (u => u.Type == team);
            else return (u => u.Type != team);
        }
        else {
            return (u => true);
        }
    }
}

