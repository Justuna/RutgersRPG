using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ModifierWrapper
{
    int _turnsLeft;
    Unit _user;
    Unit _target;
    bool _permanent;

    public Unit User { get { return _user; } }
    public Unit Target { get { return _target; } }

    public ModifierWrapper(int turns, Unit user, Unit target)
    {
        _turnsLeft = turns;
        _user = user;
        _target = target;
    }

    public bool CompleteTurn()
    {
        if (_permanent) return false;
        _turnsLeft--;
        if (_turnsLeft <= 0) return true;
        return false;
    }
}
