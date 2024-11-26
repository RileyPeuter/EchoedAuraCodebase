using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculated : TacticalAbility
{
    public Calculated() : base("Calculated", ModType.None, 0, 0, 0, 0, AbilityType.Self, 104, false, 1)
    {

    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
//        target.occupier.getCharacter().addBuff();
    }
}
