using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewUnitSpec", menuName="Specification/Unit")]
public class UnitSpec : ScriptableObject
{
    public string Name;
    
    //Quantities
    public int Health;
    public int Mana;

    //Stat Block
    public float Speed;
    public float Strength;
    public float Magic;
    public float Defense;

    public TeamType Type;
    public Sprite BattleSprite;
    public List<MoveSpec> Movepool;
}
