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
    public Healthbar healthbar;

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

        healthbar.SetMaxHealth(MaxHealth);
    }

    public int SetHealth(int amount)
    {
        int initialHealth = CurrentHealth;
        CurrentHealth += amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        healthbar.UpdateHealth(CurrentHealth);
        return initialHealth - CurrentHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth == 0;
    }

    
}
