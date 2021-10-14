using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnit : Unit
{
    //Some sort of decision-making module

    public override void Initialize(UnitSpec Spec)
    {
        base.Initialize(Spec);
    }

    public MoveSpec ChooseMove()
    {
        return (base.Movepool[Random.Range(0, base.Movepool.Count)]);
    }
}
