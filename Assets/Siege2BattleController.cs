using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege2BattleController : BattleController
{
    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void interact(int index)
    {
        base.interact(index);
        switch (index)
        {
            case 1:
                exileCharacter(getActiveCharacter());

                break;
        }
    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
        List<TacticalAbility> output = new List<TacticalAbility>();

        output = base.getTacticalAbilities();

        if (getActiveCharacter().getOccupying().xPosition == 14 && getActiveCharacter().getOccupying().yPosition == 2)
        {
            Interact nInteract = new Interact(this, 1, "Survery the oncomoing forces");
            output.Add(nInteract);
        }

        return output;
    }


    public override void objectiveComplete(string id)
    {
        
    }

    public override void loadCharacters()
    {

    }
}
