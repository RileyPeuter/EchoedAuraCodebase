using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Character
{
    public Gate() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        addBuff(new Barricade(this, 30));

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Gate";
        resourceString = "Gate";
    }
}
