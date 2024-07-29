using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeCart : Character
{

    public TradeCart() : base()
    {
        initiateBasicStats(1, 1, 1, 1, 1, 1);
        initiateDerivedStats();
        addBuff(new Inanimate(this));
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);

        CharacterName = "Trade Cart";
        resourceString = "TradeCart";
    }
}
