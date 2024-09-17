using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : TacticalAbility
{
    BattleController BC;
    int interactionID;
    string costString;

    public Interact(string nName, ModType nModType, int nBlockTH, int nParryTH, int nDodgeTH, int nBaseManaCost, AbilityType nAbilityType, int nAnimationID = 0, bool nHasSubAbilities = false, int nRange = 1) : base(nName, nModType, nBlockTH, nParryTH, nDodgeTH, nBaseManaCost, nAbilityType, nAnimationID, nHasSubAbilities, nRange)
    {
    }

    public Interact(BattleController nBC, int nInteractionID, string nDescription = "") : base("Interact", ModType.None, 0,0,0,0,AbilityType.Self, 101)
    {
        BC = nBC;
        interactionID = nInteractionID;
        description = nDescription;
    }

    public override bool isCastable(BattleCharacterObject BCO)
    {
        return true;
    }

    public override void spendMana(BattleCharacterObject BCO)
    {
        //Gonna have to change this at some point
        BCO.spendMovement(2);
    }

    public void setCostString(string cost)
    {
        costString = cost;
    }

    public override string getCostString()
    {
        return costString;
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        BC.interact(interactionID);
    }
}
