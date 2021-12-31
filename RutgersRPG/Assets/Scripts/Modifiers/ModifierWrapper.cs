using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModifierWrapper
{
    Modifier _modifier;
    int _turnsLeft;
    Unit _user;
    Unit _target;

    public ModifierWrapper(Modifier modifier, Unit user, Unit target)
    {
        _modifier = modifier;
        _turnsLeft = modifier.NumberOfTurns;
        _user = user;
        _target = target;

        if (_modifier is EffectModifier)
        {
            EffectModifier ef = (EffectModifier)_modifier;
            UnityEvent trigger = null;
            switch (ef.Event)
            {
                case EffectModifier.EventType.TAKE_DAMAGE:
                    trigger = target.TakeDamage;
                    break;
                case EffectModifier.EventType.DEAL_DAMAGE:
                    trigger = target.DealDamage;
                    break;
                case EffectModifier.EventType.GIVE_HEALING:
                    trigger = target.GiveHealing;
                    break;
                case EffectModifier.EventType.RECEIVE_HEALING:
                    trigger = target.ReceiveHealing;
                    break;
                case EffectModifier.EventType.START_TURN:
                    trigger = BattleSystem.Instance.StartTurn;
                    break;
                case EffectModifier.EventType.END_TURN:
                    trigger = BattleSystem.Instance.EndTurn;
                    break;
            }
            trigger.AddListener(delegate { BattleSystem.Instance.ReserveModifierEffect(this); });
        }
    }

    public bool CompleteTurn()
    {
        if (_modifier.Permanent) return false;
        _turnsLeft--;
        if (_turnsLeft <= 0) return true;
        return false;
    }

    public void ApplyEffect()
    {
        if (_modifier is EffectModifier)
        {
            EffectModifier ef = (EffectModifier)_modifier;
            ef.ApplyEffect(_user, _target);
        }
    }
}
