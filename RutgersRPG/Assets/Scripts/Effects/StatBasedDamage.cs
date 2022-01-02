using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatBasedDamage", menuName = "Effect/Stat-Based HP Change")]
public class StatBasedDamage : Effect
{
    public Stat BaseStat;
    public float Percent;
    public float BaseAmount;
    public bool Healing;

    public override void Apply(Unit user, Unit target)
    {
        float amount = user.GetStat(BaseStat);
        amount = (amount + BaseAmount) * Percent;
        if (!Healing) amount *= -1;

        target.ChangeHealth((int)amount);
        if (amount > 0) user.GiveHealing.Invoke();
        else user.DealDamage.Invoke();
    }
}
