using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Distraction : TacticalAbility, BattleEventListener
{

    public override List<ExWhyCell> getCustomRange()
    {
        List<ExWhyCell> output = new List<ExWhyCell>();
        if (lastMiss != null)
        {
            output.Add(lastMiss.getOccupying());
        }
        return output;
    }


    BattleController BC;
    BattleCharacterObject lastMiss;
    string reactionUsed;

    public Distraction(BattleController nBC) : base("Just A Distraction", ModType.None, 1,1,1, 1, AbilityType.Support, 103, false)
    {
        BC = nBC;
        rangeMode = RangeMode.Custom;
        friendly = true;
        description = "It was just a distraction! Follow up with the REAL attack";
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        BattleCharacterObject BCO = BC.getActiveCharacter();


        Ability abilityToUse = BC.getActiveCharacter().getAllAbilities().Find(x => x.name == "MeleeStrike");
        if (abilityToUse is null)
        {
            abilityToUse = BC.getActiveCharacter().getAllAbilities().Find(x => x.name == "RangedStrike");
        }

        if (abilityToUse is null)
        {
            Debug.Log("Hey fam, you used Distraction with a character that has neither Melee or Ranged strike");
            return;
        }

        Assert.IsTrue(lastMiss.getOccupying() != null);
        switch (reactionUsed)
        {
            case "Dodge":
                target.occupier.getCharacter().addBuff(new DodgeDisabled(target.occupier.getCharacter()));
            break;

            case "Block":
                target.occupier.getCharacter().addBuff(new BlockDisabled(target.occupier.getCharacter()));
            break;

            case "Parry":
                target.occupier.getCharacter().addBuff(new ParryDisabled(target.occupier.getCharacter()));
             break;
        }

        BC.forceCast(BC.getActiveCharacter(), lastMiss.getOccupying(), abilityToUse);


    }

    public void hearEvent(BattleEvent nBattleEvent)
    {
        if (nBattleEvent.eventType == BattleEventType.EndTurn)
        {
            lastMiss = null;
        }

        if(nBattleEvent.eventType != BattleEventType.React)
        {
            return;
        }

        if(nBattleEvent.result == "True")
        {
            return;
        }

        BattleCharacterObject BCO = BC.getCharacterFromName(nBattleEvent.subject);

        reactionUsed = nBattleEvent.verb;

        if (BCO is null) { return; }

        lastMiss = BCO;

    }

}
