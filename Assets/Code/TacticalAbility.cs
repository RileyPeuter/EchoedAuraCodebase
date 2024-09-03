using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TacticalAbility : Ability
{
    int tacticalPointCost = 1;

    protected TacticalAbility(string nName, ModType nModType, int nBlockTH, int nParryTH, int nDodgeTH, int nBaseManaCost, AbilityType nAbilityType, int nAnimationID = 0, bool nHasSubAbilities = false, int nRange = 1) : base(nName, nModType, nBlockTH, nParryTH, nDodgeTH, nBaseManaCost, nAbilityType, nAnimationID, nHasSubAbilities, nRange)
    {

    }

    public override string getCostString()
    {
        return "T:" + tacticalPointCost;
    }

    public override void spendMana(BattleCharacterObject BCO)
    {
        BattleController.ActiveBattleController.spendTacticalPoints(tacticalPointCost);
    }

    public override bool isCastable(BattleCharacterObject BCO)
    {
        if(BattleController.ActiveBattleController.tacticalPoints > 0)
        {
            return true;
        }
        return false;
    }

}
