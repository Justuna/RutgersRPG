using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUnit : Unit
{
    // Start is called before the first frame update
    public MoveMenu Menu;
    
    public override void Initialize(UnitSpec Spec)
    {
        base.Initialize(Spec);
        Menu.AddMenuItems(Movepool);
    }
}
