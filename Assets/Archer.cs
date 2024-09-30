using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    public Archer() : base() {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Foreign Archer";
        resourceString = "Archer";

        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new RangedStrike());

        DefaultCAI = new LineDefenceAI();
        reactionsAvailable = new List<reactionType> { reactionType.Block };
}
}
