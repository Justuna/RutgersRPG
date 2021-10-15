using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUnit : Unit
{
    // Start is called before the first frame update
    public MoveMenu Menu;
    public ResourceMeter Manabar;

    public int CurrentMana;
    public int MaxMana;

    public override void Initialize(UnitSpec Spec)
    {
        base.Initialize(Spec);
        CurrentMana = MaxMana = Spec.Mana;
        Manabar.SetMaxValue(MaxMana);
        Menu.AddMenuItems(Movepool);
    }

    public int GetMana() {
        return CurrentMana;
    }

    public void SetMana(int amount) {
        CurrentMana += amount;
        if (CurrentMana < 0) CurrentMana = 0;
        if (CurrentMana > MaxMana) CurrentMana = MaxMana;

        if (Manabar != null) Manabar.UpdateValue(CurrentMana);
    }
}
