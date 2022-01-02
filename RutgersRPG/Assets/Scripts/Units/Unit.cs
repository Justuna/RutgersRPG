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

    public int Threat;

    public float BaseSpeed;
    public float BaseStrength;
    public float BaseMagic;
    public float BaseDefense;

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
        BaseSpeed = Spec.Speed;
        BaseStrength = Spec.Strength;
        BaseMagic = Spec.Magic;
        BaseDefense = Spec.Defense;
        Type = Spec.Type;
        ChildSprite.sprite = Spec.BattleSprite;
        Movepool = Spec.Movepool;
        Modifiers = new List<ModifierWrapper>();

        Healthbar.SetMaxValue(MaxHealth);
    }

    public float GetStat(Stat stat)
    {
        float flatAdd = 0;
        float addPercent = 1;
        float multPercent = 1;
        float baseStat = 0;

        switch (stat)
        {
            case Stat.STRENGTH:
                baseStat = BaseStrength;
                break;
            case Stat.SPEED:
                baseStat = BaseSpeed;
                break;
            case Stat.MAGIC:
                baseStat = BaseMagic;
                break;
            case Stat.DEFENSE:
                baseStat = BaseDefense;
                break;
        }

        foreach (ModifierWrapper mw in Modifiers)
        {
            if (mw is StatModifierWrapper)
            {
                StatModifierWrapper smw = (StatModifierWrapper)mw;
                StatModifier modifier = smw.GetStatMod();
                if (modifier.stat == stat)
                {
                    switch(modifier.type)
                    {
                        case StatModifier.ModifierType.FLAT:
                            flatAdd += modifier.amount;
                            break;
                        case StatModifier.ModifierType.ADD_PERCENT:
                            addPercent += modifier.amount;
                            break;
                        case StatModifier.ModifierType.MULTI_PERCENT:
                            multPercent *= modifier.amount;
                            break;
                    }
                }
            }
        }

        float amount = (baseStat + flatAdd) * addPercent * multPercent;
        //Debug.Log(Name + "'s current " + stat.ToString() + " is " + amount + ".");
        return amount;
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
