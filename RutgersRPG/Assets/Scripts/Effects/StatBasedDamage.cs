using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatBasedDamage", menuName = "Effect/Stat-Based HP Change")]
public class StatBasedDamage : Effect
{
    public Stat AttackStat;
    public Stat DefenseStat;
    public float BaseDamage;

    public override void Apply(Unit user, Unit target)
    {
        float amount = user.GetStat(AttackStat)/target.GetStat(DefenseStat);
        amount *= BaseDamage * -1;

        target.ChangeHealth((int)amount);
        if (amount > 0) user.GiveHealing.Invoke();
        else user.DealDamage.Invoke();
    }
}
