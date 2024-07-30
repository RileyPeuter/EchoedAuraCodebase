using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : TacticalAbility, BattleEventListener
{
    public Distraction(string nName, ModType nModType, int nBlockTH, int nParryTH, int nDodgeTH, int nBaseManaCost, AbilityType nAbilityType, int nAnimationID = 0, bool nHasSubAbilities = false, int nRange = 1) : base(nName, nModType, nBlockTH, nParryTH, nDodgeTH, nBaseManaCost, nAbilityType, nAnimationID, nHasSubAbilities, nRange)
    {
    }

    public void hearEvent(BattleEvent nBattleEvent)
    {

    }
}
