using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryAI : BattleCharacterAI
{
    bool attacked = false;
    public override Ability getAbility()
    {
        if (attacked) { return null; }
        return getAvailableAbilities()[0];
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        return null;
    }
}
