using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointAI : BattleCharacterAI
{
    ExWhyCell targetCell;
    bool targeting = false;
    bool immediateTargeting = false;

    public void setTargetCell(ExWhyCell nTargetCell)
    {
        targetCell = nTargetCell;
        targeting = true;
    }
    public override Ability getAbility()
    {
        Ability output = getAvailableAbilities().Find(x => x.name == "MeleeStrike");

        if(output is null)
        {
            return null;
        }

        if(Random.Range(0,2) < 1) {
            targeting = true;
        }

        /*
        if (targeting)
        {
            immediateTargeting = targeting;
            if (!BattleController.ActiveBattleController.getCastable(output))
            {
                immediateTargeting = false;
                if (!BattleController.ActiveBattleController.getCastable(output)){
                    output = BCO.getMovementAbility();

                }
            }
        }
        else
        {
        */
        if(!BattleController.ActiveBattleController.getCastable(output))
        {
            output = BCO.getMovementAbility();
        }
//        }


        return output;
    }

    public override reactionType getReaction()
    {
        return reactionType.Dodge;
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        ExWhyCell output = null;
        if (immediateTargeting)
        {
           switch (ability.name)
            {
                case "Move":
                    output = moveTowardsAlliedAndControlled(true);
                    break;

                case "MeleeStrike":
                    output = getTarget(BattleController.ActiveBattleController.AR.findCharactersInRange());
                    break;
            }
        }

        else
        {
            switch (ability.name)
            {   
                case "Move":
                    output = moveTowardsAlliedAndControlled(true);
                    break;

                case "MeleeStrike":
                    output = getTarget(BattleController.ActiveBattleController.AR.findCharactersInRange());
                    break;
            }
        }
        immediateTargeting = false;
        return output;
    }

    public virtual ExWhyCell moveTowardsTarget(bool requiresAvailable = false)
    {
        return AbilityRange.getClosestCell(targetCell, BattleController.ActiveBattleController.AR.findCellsInRange(RangeMode.Move), requiresAvailable);
    }

}
