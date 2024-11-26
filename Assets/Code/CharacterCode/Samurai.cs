using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Character
{
    public Samurai() : base() {
        initiateBasicStats(2, 4, 3, 3, 4, 2);
        initiateDerivedStats();


        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Foreign Swordsman";
        resourceString = "Samurai";

        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());

        reactionsAvailable = new List<reactionType> { reactionType.Parry };

        DefaultCAI = new StationaryAI();
    
    }
}
