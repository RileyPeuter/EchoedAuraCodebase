using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Ability
{


    public Move() : base("Move", ModType.Ranged, 10, 10, 5, 2, AbilityType.Area)
    {
        rangeMode = RangeMode.Move;
    }
    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {
        caster.spendMovement(ExWhy.getDistanceBetweenCells(caster.getOccupying(), target));
        caster.move(target);
    }

    //Alright, lads, this shit is gonna be some very Spaghetti Code
    public override int GetRange()
    {
        return GameObject.Find("MapController").GetComponent<BattleController>().getActiveCharacter().getMovement();
    }

}