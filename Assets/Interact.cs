using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : TacticalAbility
{
    BattleController BC;

    public Interact(string nName, ModType nModType, int nBlockTH, int nParryTH, int nDodgeTH, int nBaseManaCost, AbilityType nAbilityType, int nAnimationID = 0, bool nHasSubAbilities = false, int nRange = 1) : base(nName, nModType, nBlockTH, nParryTH, nDodgeTH, nBaseManaCost, nAbilityType, nAnimationID, nHasSubAbilities, nRange)
    {
    }

    public Interact(BattleController nBC) : base("Interact", ModType.None, 0,0,0,0,AbilityType.Self)
    {
        BC = nBC;
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {
        BC.interact(0);
    }
}
