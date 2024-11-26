using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extract : TacticalAbility
{
    BattleController BC;
    public Extract(BattleController nBC) : base("Extraction", ModType.None, 0, 0, 0, 0, AbilityType.Self, 106, false, 1)
    {
        BC = nBC;
        description = "Withdraw from the battlefield";
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        BC.exileCharacter(caster);
    }

    public override void spendMana(BattleCharacterObject BCO)
    {
        //Gonna have to change this at some point
//        BCO.spendMovement(2);
    }

    public override bool isCastable(BattleCharacterObject BCO)
    {
        return true;
    }
}
