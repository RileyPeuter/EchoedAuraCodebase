using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruless : Character
{
    public Ruless() : base() {
        initiateBasicStats(2, 2, 2, 2, 2, 2);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Ruless";
        resourceString = "Ruless";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());

    }

    


}
