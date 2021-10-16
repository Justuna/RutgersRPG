using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public MoveSpec move;
    public PCUnit unit;

    public MoveSelectedEvent _moveSelectedEvent = default;

    private void Awake() {
        _moveSelectedEvent.AddListener(BattleSystem.Instance.RecordMove);
    }

    public void SelectMove()
    {
        if (move.CheckCost(unit)) _moveSelectedEvent.Invoke(move);
    }
}
