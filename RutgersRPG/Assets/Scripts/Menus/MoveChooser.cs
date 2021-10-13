using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChooser : MonoBehaviour
{
    public MoveSpec move;
    public PCUnit unit;

    public MoveChosenEvent moveChosenEvent = default;

    public void ChooseMove()
    {
        if (move.CheckCost(unit)) moveChosenEvent.Invoke(move);
    }
}
