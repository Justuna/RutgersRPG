using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public List<ModifierWrapper> Modifiers;

    public UnityEvent TakeDamage = new UnityEvent();
    public UnityEvent DealDamage = new UnityEvent();
    public UnityEvent ReceiveHealing = new UnityEvent();
    public UnityEvent GiveHealing = new UnityEvent();

    public virtual void Initialize(UnitSpec Spec)
    {
        this.Spec = Spec;
        Name = Spec.Name;
        CurrentHealth = MaxHealth = Spec.Health;
        Speed = Spec.Speed;
        Type = Spec.Type;
        ChildSprite.sprite = Spec.BattleSprite;
        Movepool = Spec.Movepool;
        Modifiers = new List<ModifierWrapper>();

        Healthbar.SetMaxValue(MaxHealth);
    }

    public int GetSpeed() {
        return Speed;
    }

    public int ChangeHealth(int amount)
    {
        if (amount <= 0) TakeDamage.Invoke();
        else ReceiveHealing.Invoke();

        int initialHealth = CurrentHealth;
        CurrentHealth += amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        Healthbar.UpdateValue(CurrentHealth);
        return initialHealth - CurrentHealth;
    }

    public void AddModifier(ModifierWrapper modifier)
    {
        Modifiers.Add(modifier);
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
