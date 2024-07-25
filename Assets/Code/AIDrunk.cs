using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDrunk : BattleCharacterAI
{
    //Drunk should never react
    public override Ability getAbility()
    {
        return null;
    }

    public override reactionType getReaction()
    {
        return reactionType.Dodge;
    }

    public override ExWhyCell getTarget(Ability ability, AbilityRange AR)
    {
        return null;
    }

}
