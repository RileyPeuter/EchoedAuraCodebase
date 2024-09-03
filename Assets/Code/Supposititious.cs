using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supposititious : TacticalAbility, BattleEventListener
{

    BattleController BC;
    Dictionary<BattleCharacterObject, int> lastCharacterDamages = new Dictionary<BattleCharacterObject, int>();

    public Supposititious(BattleController nBC) : base("Supposititious Technique", ModType.None, 0, 0, 0, 1, AbilityType.Targeted, 102, false)
    {
        BC = nBC;
        rangeMode = RangeMode.Custom;
        friendly = true;
        description = "It was a Phantom all along! Removes the last instance of damage from target.";
    }

    public override List<ExWhyCell> getCustomRange()
    {
        List<ExWhyCell> output = new List<ExWhyCell>();

        foreach (BattleCharacterObject character in lastCharacterDamages.Keys)
        {
            output.Add(character.getOccupying());
        }

        return output;
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        BattleCharacterObject BCO = target.occupier;
        BCO.heal(lastCharacterDamages[BCO]);
    }

    public void hearEvent(BattleEvent nBattleEvent)
    {
        if (nBattleEvent.eventType != BattleEventType.Hit)
        {
            return;
        }

        BattleCharacterObject BCO = BC.getCharacterFromName(nBattleEvent.target);
        if(BCO is null){ return; }

        if(BCO.GetAllegiance() != CharacterAllegiance.Controlled)
        {
            return;
        }

        lastCharacterDamages[BCO] = int.Parse(nBattleEvent.result);
        //lastCharacterDamages.Add(BCO, int.Parse(nBattleEvent.result))
    }
}