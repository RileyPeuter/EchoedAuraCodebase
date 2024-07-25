using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iraden : Character
{
    public Iraden() : base()
    {
        initiateBasicStats(1, 1, 2, 2, 2, 1);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Iraden";
        resourceString = "Iraden";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        CharacterAbilities.Add(new IceShuriken());
        GeneralAbilities.Add(new MeleeStrike());
        GeneralAbilities.Add(new RangedStrike());
    }
}
    