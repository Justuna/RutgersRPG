using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType {PLAYER, ENEMY, NEUTRAL, FRIENDLY}

public abstract class Unit : MonoBehaviour
{
    public string Name;
    
    public int CurrentHealth;
    public int MaxHealth;

    public int CurrentMana;
    public int MaxMana;

    public int Speed;

    public TeamType Type;

    public UnitSpec Spec;

    public SpriteRenderer ChildSprite;
    public ResourceMeter healthbar;
    public ResourceMeter manabar;

    private MoveSpec _currentMove;
    public List<MoveSpec> Movepool;

    public virtual void Initialize(UnitSpec Spec)
    {
        this.Spec = Spec;
        Name = Spec.Name;
        CurrentHealth = MaxHealth = Spec.Health;
        CurrentMana = MaxMana = Spec.Mana;
        Speed = Spec.Speed;
        Type = Spec.Type;
        ChildSprite.sprite = Spec.BattleSprite;
        Movepool = Spec.Movepool;

        if (manabar != null) manabar.SetMaxValue(MaxMana);
        healthbar.SetMaxValue(MaxHealth);
    }

    public void SetMove(MoveSpec move) {
        _currentMove = move;
    }

    public MoveSpec GetMove() {
        return _currentMove;
    }

    public void UseMove(Unit user, Unit target) {
        SetMana(-_currentMove.GetCost());
        _currentMove.UseMove(user, target);
    }

    public int GetSpeed() {
        return Speed;
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

    public int SetHealth(int amount)
    {
        int initialHealth = CurrentHealth;
        CurrentHealth += amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        healthbar.UpdateValue(CurrentHealth);
        return initialHealth - CurrentHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth == 0;
    }

    
}
