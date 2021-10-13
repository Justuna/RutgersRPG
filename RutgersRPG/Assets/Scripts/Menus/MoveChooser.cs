using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChooser : MonoBehaviour
{
    public MoveSpec move;

    public MoveChosenEvent moveChosenEvent = default;

    public void ChooseMove()
    {
        moveChosenEvent.Invoke(move);
    }
}
