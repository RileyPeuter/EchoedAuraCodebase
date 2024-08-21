using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorcia : Character
{
    public Dorcia (): base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        CharacterName = "Dorcia";
        resourceString = "Dorcia";
    }

}
