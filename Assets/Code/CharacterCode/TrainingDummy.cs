using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : Character
{

    public TrainingDummy() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        addBuff(new Inanimate(this));
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Training Dummy";
        resourceString = "TrainingDummy";

        reactionsAvailable = new List<reactionType>();
    }
}
