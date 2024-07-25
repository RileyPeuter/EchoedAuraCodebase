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

        if (targeting)
        {
            immediateTargeting = true;
            if (!BattleController.ActiveBattleController.getCastable(output, CharacterAllegiance.Dormant))
            {
                immediateTargeting = false;
                if (!BattleController.ActiveBattleController.getCastable(output)){
                    output = BCO.getMovementAbility();

                }
            }
        }
        else
        {
            if(!BattleController.ActiveBattleController.getCastable(output, CharacterAllegiance.Dormant))
            {
                output = BCO.getMovementAbility();
            }
        }


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
                    output = moveTowardsTarget(true);
                    break;

                case "MeleeStrike":
                    output = moveTowardsTarget(false);
                    break;
            }
        }
        else
        {
            switch (ability.name)
            {   
                case "Move":
                    output = moveTowardsEnemy(true);
                    break;

                case "MeleeStrike":
                    output = moveTowardsEnemy(false);
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
