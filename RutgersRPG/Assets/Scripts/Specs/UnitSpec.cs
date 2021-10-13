using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewUnitSpec", menuName="Specification/Unit")]
public class UnitSpec : ScriptableObject
{
    public string Name;
    public int Health;
    public int Mana;
    public int Speed;
    public TeamType Type;
    public Sprite BattleSprite;
    public List<MoveSpec> Movepool;
}