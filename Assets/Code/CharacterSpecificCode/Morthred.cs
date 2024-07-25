using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morthred : Character 
{
    public Morthred() : base()
    {
        initiateBasicStats(1, 2, 3, 1, 2, 4);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Morthred";
        resourceString = "Morthred";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new RangedStrike());
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
