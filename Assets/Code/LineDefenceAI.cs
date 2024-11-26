using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDefenceAI : BattleCharacterAI
{
    bool mobile = false;

    public LineDefenceAI(bool nMobile)
    {
        mobile = nMobile;
    }

    public LineDefenceAI()
    {
    }


    public override Ability getAbility()
    {
        if (alerted)
        {
            mobile = true;
        }

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
                return getClosestTarget(new List<CharacterAllegiance>() {CharacterAllegiance.Controlled});
            break;
        
        }
        return null;
    }
}
