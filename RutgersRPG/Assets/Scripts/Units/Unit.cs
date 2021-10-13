using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType {PLAYER, ENEMY, NEUTRAL, FRIENDLY}

public abstract class Unit : MonoBehaviour
{
    public string Name;
    
    public int CurrentHealth;
    public int MaxHealth;

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
        Speed = Spec.Speed;
        Type = Spec.Type;
        ChildSprite.sprite = Spec.BattleSprite;
        Movepool = Spec.Movepool;

        healthbar.SetMaxValue(MaxHealth);
    }

    public void SetMove(MoveSpec move) {
        _currentMove = move;
    }

    public MoveSpec GetMove() {
        return _currentMove;
    }

    public virtual void UseMove(Unit user, Unit target) {
        _currentMove.UseMove(user, target);
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

        healthbar.UpdateValue(CurrentHealth);
        return initialHealth - CurrentHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth == 0;
    }

    
}
