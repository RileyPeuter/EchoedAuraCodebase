using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kahund : Character
{
    public Kahund () : base () {
        initiateBasicStats(2, 2, 2, 2, 2, 2);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Angry Kahund";
        resourceString = "Kahund";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());
        
        DefaultCAI = new AttackPointAI();
    }

    public Kahund(ExWhyCell target) : base()
    {
        initiateBasicStats(2, 2, 2, 2, 2, 2);
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
    }


}
