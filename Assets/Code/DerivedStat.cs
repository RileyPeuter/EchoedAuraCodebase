using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum derivedStat
{
    movement,
    maxHealthPoints,
    maxManaPoints,
    manaFlow,
    meleeBonus,
    rangedBonus,
    magicBonus,
    block,
    dodge,
    parry,
    blockTH,
    dodgeTH,
    parryTH,
    turnFrequency
}

public class DerivedStat
{
    //###Member Variables###
    public derivedStat derivedStatType;
    public int baseFromMain;

    public Dictionary<int, int> additionBuffs;
    public Dictionary<int, int> multiplictiveBuffs;

    //###Utilities###
    public int calculateStat()
    {
        int output = baseFromMain;
        foreach (KeyValuePair<int, int> values in additionBuffs)
        {
            output = output + values.Value;
        }

        //Gonna have to make this floaty at some point
        foreach (KeyValuePair<int, int> values in multiplictiveBuffs)
        {
            output = output * values.Value;
        }

        return output;
    }

    public void addAdditiveBuff(int ID, int mag)
    {
        additionBuffs.Add(ID, mag);
    }

    public void removeBuff(int ID)
    {
        additionBuffs.Remove(ID);
        multiplictiveBuffs.Remove(ID);
    }

    //###Constructor###
    public DerivedStat(derivedStat nDerivedStatType, int mainStat0, int mainStat1 = 0, int baseLine = 0)
    {
        derivedStatType = nDerivedStatType;
        baseFromMain = mainStat0 + mainStat1 + baseLine;
        additionBuffs = new Dictionary<int, int>();
        multiplictiveBuffs = new Dictionary<int, int>();
    }
    
    public DerivedStat(derivedStat nStatType, int nBaseAmount)
    {
        derivedStatType = nStatType;
        baseFromMain = nBaseAmount;
        additionBuffs = new Dictionary<int, int>();
        multiplictiveBuffs = new Dictionary<int, int>();
    }

    public void reCalculateBaseFromMain(int mainstat0, int mainstat1 = 0, int baseline = 0)
    {
        baseFromMain = mainstat0 + mainstat1 + baseline;    
    }

    public void setBase(int nBase)
    {
        baseFromMain = nBase;
    }
}