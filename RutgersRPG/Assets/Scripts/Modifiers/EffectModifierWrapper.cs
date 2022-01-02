using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectModifierWrapper : ModifierWrapper
{
    EffectModifier _modifier;

    public EffectModifierWrapper(EffectModifier modifier, Unit user, Unit target) : base(modifier.NumberOfTurns, user, target)
    {
        _modifier = modifier;
       
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

    public void ApplyEffect()
    {
        _modifier.ApplyEffect(base.User, base.Target);
    }
}
