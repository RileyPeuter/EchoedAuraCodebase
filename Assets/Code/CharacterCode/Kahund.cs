using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kahund : Character
{
    public Kahund () : base () {
        initiateBasicStats(2, 0, 2, 2, 2, 2);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Angry Kahund";
        resourceString = "Kahund";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());

        defaultVision = 2;

        DefaultCAI = new AttackPointAI();
        reactionsAvailable = new List<reactionType>() { reactionType.Dodge };
    }

    public Kahund(ExWhyCell target) : base()
    {
        initiateBasicStats(2,0, 2, 2, 2, 2);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Hungry Kahund";
        resourceString = "Kahund";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());
        AttackPointAI APAI = new AttackPointAI();

        APAI.setTargetCell(target);

        DefaultCAI = APAI;

        reactionsAvailable = new List<reactionType>() { reactionType.Dodge };
    }


}
