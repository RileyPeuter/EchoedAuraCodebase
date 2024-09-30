using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDefenceAI : BattleCharacterAI
{
    bool mobile = false;
    bool moved = false;

    public override Ability getAbility()
    {
        foreach(Ability ability in getAvailableAbilities())
        {
            if(BattleController.ActiveBattleController.getCastable(ability, CharacterAllegiance.Controlled))
            {
                return ability;
            }
        }

        if (mobile && !moved) {
            return BCO.getMovementAbility();   
        }

        return null;
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        switch (ability.name) {

            case "Move":
                return moveTowardsControlled(true);
             break;

            case "MeleeStrike":
            case "RangedStrike":
                return moveTowardsControlled();
            break;
        
        }
        return null;
    }
}
