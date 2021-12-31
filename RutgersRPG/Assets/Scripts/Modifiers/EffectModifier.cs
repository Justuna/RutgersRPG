using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EffectModifierSpec", menuName = "Modifier/Effect Modifier")]
public class EffectModifier : Modifier
{
    public enum EventType { TAKE_DAMAGE, DEAL_DAMAGE, GIVE_HEALING, RECEIVE_HEALING, END_TURN, START_TURN }

    public string Name;
    public List<Effect> Effects;
    public EventType Event;

    public void ApplyEffect(Unit user, Unit target)
    {
        if (target.IsDead()) return;
        foreach (Effect e in Effects)
        {
            e.Apply(user, target);
            Debug.Log(target.Name + " was affected by " + Name + "!");
        }
    }
}
