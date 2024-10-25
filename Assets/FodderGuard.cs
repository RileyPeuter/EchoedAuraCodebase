using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderGuard : Character
{
    public FodderGuard() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1,1);
        initiateDerivedStats();

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Peasant Rebel";
        resourceString= "FodderGuard";

        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new MeleeStrike());

        DefaultCAI = new LineDefenceAI();
        reactionsAvailable = new List<reactionType> { reactionType.Block };

        defaultVision = 3;
    }
}
