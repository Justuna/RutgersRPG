using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType {PLAYER, ENEMY}

public abstract class Unit : MonoBehaviour
{
    public string Name;
    
    public int CurrentHealth;
    public int MaxHealth;

    public int Speed;
    public int Threat;

    public TeamType Type;

    public UnitSpec Spec;

    public SpriteRenderer ChildSprite;
    public ResourceMeter Healthbar;
    public UnitSelector Selector;

    public List<MoveSpec> Movepool;

    public virtual void Initialize(UnitSpec Spec)
    {
        this.Spec = Spec;
        Name = Spec.Name;
        CurrentHealth = MaxHealth = Spec.Health;
        Speed = Spec.Speed;
        Type = Spec.Type;
        ChildSprite.sprite = Spec.BattleSprite;
        Movepool = Spec.Movepool;

        Healthbar.SetMaxValue(MaxHealth);
    }

    public int GetSpeed() {
        return Speed;
    }

    public int SetHealth(int amount)
    {
        int initialHealth = CurrentHealth;
        CurrentHealth += amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        Healthbar.UpdateValue(CurrentHealth);
        return initialHealth - CurrentHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth == 0;
    }

    public void DisplaySelection() {
        Selector.PotentialTarget();
    }

    public void HideSelection() {
        Selector.NoLongerTarget();
    }
}
