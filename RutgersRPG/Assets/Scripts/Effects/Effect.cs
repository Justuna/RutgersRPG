using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Effect : ScriptableObject
{
    public abstract void Apply(Unit user, Unit target);
}
