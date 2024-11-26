using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    public Archer(bool flag = false) : base() {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Foreign Archer";
        resourceString = "Archer";

        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new RangedStrike(5));

        DefaultCAI = new LineDefenceAI();
        if (flag)
        {
            DefaultCAI = new LineDefenceAI(true);
        }
        reactionsAvailable = new List<reactionType> { reactionType.Dodge };

        defaultVision = 4;
    }

}
