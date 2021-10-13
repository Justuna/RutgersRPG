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

    public void ChooseMove()
    {
        base.SetMove(base.Movepool[(int)Mathf.Round(Random.value)]);
    }
}
