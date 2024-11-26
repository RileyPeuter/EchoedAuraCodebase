using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimAI : BattleCharacterAI
{
    bool hit = false;
    public override Ability getAbility()
    {
        Ability output = BCO.getMovementAbility();
        Ability potential = getAvailableAbilities().Find(x => x.name == "MeleeStrike");
        if (potential != null)
        {
            if (BattleController.ActiveBattleController.getCastable(potential, CharacterAllegiance.Controlled))
            {
                return potential;
            }
        }

        if (moved)
        {
            moved = false;
            return null;
        }

        moved = true;
        return output;
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        ExWhyCell output = null;
        if (ability.name == "Move") { output = moveTowardsControlled(true); }

        if (ability.name == "MeleeStrike") { output = moveTowardsControlled();};


        return output;
    }
}
