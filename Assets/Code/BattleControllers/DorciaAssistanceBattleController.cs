using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorciaAssistanceBattleController : BattleController
{

    BattleCharacterObject dorciaObject;
    public override void endBattle()
    {
        
    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void loadCharacters()
    {

    }

    public override void objectiveComplete(string id)
    {

    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
        List <TacticalAbility> output = new List<TacticalAbility>();
        output.AddRange(baseTacticalAbilities);

        if(ExWhy.getDistanceBetweenCells(getActiveCharacter().getOccupying(),dorciaObject.getOccupying()) == 1)
        {
            output.Add(new Interact(this));
        }

        return base.getTacticalAbilities();
    }

    public override void interact(int index)
    {
        dorciaObject.move(map.gridObject.gridCells[dorciaObject.getOccupying().xPosition + 1, dorciaObject.getOccupying().yPosition]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
