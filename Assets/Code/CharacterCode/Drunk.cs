using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunk : Character
{
    public Drunk() : base(){

    initiateBasicStats(2, 2, 8, 4, 2, 7);
    initiateDerivedStats();
    HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
    ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
    CharacterName = "Drunk";
    resourceString = "Drunk";
    GeneralAbilities = new List<Ability>();
    CharacterAbilities = new List<Ability>();
    DefaultCAI = new AIDrunk();
    }
}
