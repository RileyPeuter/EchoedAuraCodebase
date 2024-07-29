using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTarget : Character
{

    public TrainingTarget() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        addBuff(new Inanimate(this));

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Training Target";
        resourceString = "TrainingTarget";
    }
}
