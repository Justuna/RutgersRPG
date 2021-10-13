using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUnit : Unit
{
    // Start is called before the first frame update
    public MoveMenu Menu;

    public int CurrentMana;
    public int MaxMana;

    public override void Initialize(UnitSpec Spec)
    {
        base.Initialize(Spec);
        CurrentMana = MaxMana = Spec.Mana;
        manabar.SetMaxValue(MaxMana);
        Menu.AddMenuItems(Movepool);
    }

    public override void UseMove(Unit user, Unit target) {
        SetMana(-GetMove().GetCost());
        base.UseMove(user, target);
    }

    public int GetMana() {
        return CurrentMana;
    }

    public void SetMana(int amount) {
        CurrentMana += amount;
        if (CurrentMana < 0) CurrentMana = 0;
        if (CurrentMana > MaxMana) CurrentMana = MaxMana;

        if (manabar != null) manabar.UpdateValue(CurrentMana);
    }
}
