using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrubs : Character
{
    public Shrubs() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        addBuff(new Barricade(this, 15));

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Shrub";
        resourceString = "Shrub";
    }


}
