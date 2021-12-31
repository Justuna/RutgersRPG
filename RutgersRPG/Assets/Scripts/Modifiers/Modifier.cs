using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    public bool Permanent;
    public bool Stackable;
    public int NumberOfTurns;
}
