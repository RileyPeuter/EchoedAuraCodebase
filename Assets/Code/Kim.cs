using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kim : Character
{
    public Kim() : base()
    {
        initiateBasicStats(2, 2, 2, 1, 1, 1);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Kim";
        resourceString = "Kim";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        DefaultCAI = new KimAI();

        GeneralAbilities.Add(new MeleeStrike());
        CharacterAbilities.Add(new ShikariKick());
    }
}

public class ShikariKick : Ability
{
    public ShikariKick() : base("ShikariKick", ModType.Melee, 1, 1, 1, 2, AbilityType.Targeted, 1, false, 1)
    {
        description = "A kick that launches the victim backwards";
        baseDamage = 3;
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {

        int targetXCoordinate = target.xPosition;
        int targetYCoordinate = target.yPosition;

        if (target.xPosition - caster.getOccupying().xPosition == -1)
        {
            targetXCoordinate--;
        }
        if (target.yPosition - caster.getOccupying().yPosition == -1)
        {
            targetYCoordinate--;
        }
        if (target.xPosition - caster.getOccupying().xPosition == 1)
        {
            targetXCoordinate++;
        }
        if (target.yPosition - caster.getOccupying().yPosition == 1)
        {
            targetYCoordinate++;
        }

        if (!ExWhy.activeExWhy.checkIfCordsAreWalkable(targetXCoordinate, targetYCoordinate))
        {
            return;
        }
        target.occupier.move(ExWhy.activeExWhy.gridCells[targetXCoordinate, targetYCoordinate]);


        //base.cast(target, caster, SOF);
    }
}