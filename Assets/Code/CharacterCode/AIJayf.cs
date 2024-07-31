using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJayf : BattleCharacterAI
{
    public override Ability getAbility()
    {
        Ability output = null;
        if (overrideAbility != null) { return overrideAbility; }

        switch (mode)
        {
            case AIMode.TankUp:

                output = getAvailableAbilities().Find(x => x.name == "ArsAmagus");
                tankedUp = true;
                setAIMode(AIMode.RecklessAttack);
                return output;
                break;

            default:
                if (Random.Range(0, 2) < 1)
                {
                    output = getAvailableAbilities().Find(x => x.name == "MeleeStrike");
                }
                else
                {
                    output = getAvailableAbilities().Find(x => x.name == "RangedStrike");
                }
                break;
        }
        if (output != null)
        {
            if (!BattleController.ActiveBattleController.getCastable(output))
            {
                if (BCO.getMovement() > 0) { output = BCO.getMovementAbility(); }
            }
            if (!output.isCastable(BCO))
            {
                output = null;
            }
        }
        return output;
    }

    public override reactionType getReaction()
    {
        if(Random.Range(0, 2) < 1)
        {
            return reactionType.Block;
        }
        return reactionType.Parry;
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        if(ability.name == "Move") { return moveTowardsControlled(true); }

        return GetMurderousTarget(BattleController.ActiveBattleController.AR.findCharactersInRange(), murderousIntent);

        switch (ability.name)
        {
            case "ArsAmagus":

                break;
            //case "RangedStrike":
              //  if (BattleController.ActiveBattleController.activeCharacters[moveTowardsEnemy().occupier] != CharacterAllegiance.Enemey) {
                //    return moveTowardsEnemy();
               // }
               // break;
            case "MeleeStrike":
                return moveTowardsControlled();
        }
        return null;
    }
}
