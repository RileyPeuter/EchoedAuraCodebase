using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character {
    //###MemberVariables

    public List<reactionType> reactionsAvailable = new List<reactionType>() {reactionType.Block, reactionType.Dodge, reactionType.Parry};

    public int defaultVision = 8;
    public string resourceString;
    int healthPoints;
    int manaPoints;
    Stat[] basicStats;
    DerivedStat[] derivedStats;
    public List<Ability> GeneralAbilities;
    public List<Ability> CharacterAbilities;
    public List<Buff> buffs;   
    string characterName;
    BattleCharacterObject BCO;

    protected BattleCharacterAI DefaultCAI = null;


    public int HealthPoints { get => healthPoints; set => healthPoints = value; }
    public int ManaPoints { get => manaPoints; set => manaPoints = value; }
    public Stat[] BasicStats { get => basicStats; set => basicStats = value; }
    public DerivedStat[] DerivedStats { get => derivedStats; set => derivedStats = value; }
    public string CharacterName { get => characterName; set => characterName = value; }

    //###Getter###
    public int getCurrentHealth()
    {
        return healthPoints;
        //kappa
    }

    public int getCurrentMana()
    {
        return manaPoints;
    }
    
    
    public string getResourceString()
    {
        return resourceString;
    }

    public BattleCharacterAI getDefualtBCAI()
    {
        return DefaultCAI;
    }

    public int getBasicStat(stat statType)
    {
        foreach (Stat s in basicStats)
        {
            if (s.statType == statType)
            {
                return s.calculateStat();
            }
        }
        return 0;
    }

    public Stat getBasicStatObject(stat statType)
    {

        foreach (Stat s in basicStats)
        {
            if (s.statType == statType)
            {
                return s;
            }
        }
        return null;
    }

    public int getDerivedStat(derivedStat statType)
    {
        foreach (DerivedStat stat in derivedStats)
        {
            if (stat.derivedStatType == statType)
            {
                return stat.calculateStat();
            }
        }
        return 0;
    }

    public DerivedStat getDerivedStatObject(derivedStat statType)
    {
        foreach (DerivedStat stat in derivedStats)
        {
            if (stat.derivedStatType == statType)
            {
                return stat;
            }
        }
        return null;
    }

    //This should only be used outside of combat
    public void incrementBaseStat(stat statTarget)
    {
        getBasicStatObject(statTarget).incrementAddition();
        reCalculateDerivedStats();
    }

    public void reCalculateDerivedStats()
    {
        foreach(DerivedStat stat in derivedStats)
        {
            stat.setBase(statAlgorithm(stat.derivedStatType));
        }
    }

    //###Setter###

    //###Utility###
    public void cleanUpBuffs()
    {   

    }
    public void initializeMisc()
    {
        buffs = new List<Buff>();
    }

    //Maybe add tick if we want that. 
    public void addBuff(Buff buff)
    {
        if (buff.stackable)
        {
            foreach(Buff cBuff in buffs)
            {
                if(buff.name == cBuff.name)
                {
                    cBuff.cleanup();
                    cBuff.magnitude += buff.magnitude;
                    cBuff.start();
                    return;
                }
            }
        }

        buffs.Add(buff);
        buff.start();
    }

    public void removeBuff(Buff buff) {
        buff.cleanup();
        buffs.Remove(buff);
    }

    public void dealDamage(int amount, int type)
    {
        healthPoints = healthPoints - amount;
    }

    //Alright, this big chuck of code gets just gets the derived stats from all the basic stats. 
    //Think of filling out your DndStat sheet

    public void initiateBasicStats(int str, int vitality, int speed, int precision, int focus, int ingen)
    {
        initializeMisc();
        basicStats = new Stat[]
        {
            new Stat(stat.strength, str),
            new Stat(stat.vitality, vitality),
            new Stat(stat.speed, speed),
            new Stat(stat.precision, precision),
            new Stat(stat.focus, focus),
            new Stat(stat.ingenuity, ingen)
        };
    }

    public void initiateDerivedStats()
    {
        derivedStats = new DerivedStat[]{
            new DerivedStat(derivedStat.movement, statAlgorithm(derivedStat.movement)),
            new DerivedStat(derivedStat.maxHealthPoints, statAlgorithm(derivedStat.maxHealthPoints)),
            new DerivedStat(derivedStat.maxManaPoints, statAlgorithm(derivedStat.maxManaPoints)),
            new DerivedStat(derivedStat.manaFlow, statAlgorithm(derivedStat.manaFlow)),
            new DerivedStat(derivedStat.meleeBonus, statAlgorithm(derivedStat.meleeBonus)), 
            new DerivedStat(derivedStat.rangedBonus, statAlgorithm(derivedStat.rangedBonus)),
            new DerivedStat(derivedStat.magicBonus, statAlgorithm(derivedStat.magicBonus)),
            new DerivedStat(derivedStat.block, statAlgorithm(derivedStat.block)),
            new DerivedStat(derivedStat.dodge, statAlgorithm(derivedStat.dodge)),
            new DerivedStat(derivedStat.parry, statAlgorithm(derivedStat.parry)),
            new DerivedStat(derivedStat.blockTH, statAlgorithm(derivedStat.blockTH)),
            new DerivedStat(derivedStat.dodgeTH, statAlgorithm(derivedStat.dodgeTH)),
            new DerivedStat(derivedStat.parryTH, statAlgorithm(derivedStat.parryTH)),
            new DerivedStat(derivedStat.turnFrequency, statAlgorithm(derivedStat.turnFrequency))
        };
    }

    public int statAlgorithm(derivedStat derivedStat)
    {
        int output = 0;

        switch (derivedStat) 
        {
            case derivedStat.movement:
                output = getBasicStat(stat.strength) + getBasicStat(stat.speed);
            break;

            case derivedStat.maxHealthPoints:
                output = getBasicStat(stat.vitality) + 5;
            break;

            case derivedStat.maxManaPoints:
                output = getBasicStat(stat.vitality) + getBasicStat(stat.speed) + 20;
            break;

            case derivedStat.manaFlow:
                output = getBasicStat(stat.speed) + getBasicStat(stat.focus);
            break;

            case derivedStat.meleeBonus:
                output = getBasicStat(stat.strength) + getBasicStat(stat.speed);
            break;

            case derivedStat.rangedBonus:
                output = getBasicStat(stat.precision);
            break;

            case derivedStat.magicBonus:
                output = getBasicStat(stat.ingenuity) + getBasicStat(stat.focus);
            break;

            case derivedStat.block:
                output = getBasicStat(stat.strength) + getBasicStat(stat.vitality) - 5;
            break;

            case derivedStat.dodge:
                output = getBasicStat(stat.speed) + getBasicStat(stat.ingenuity) - 5;
            break;

            case derivedStat.parry:
                output = getBasicStat(stat.precision) + getBasicStat(stat.focus);
            break;

            case derivedStat.blockTH:
                output = getBasicStat(stat.strength);
             break;

            case derivedStat.dodgeTH:
                output = getBasicStat(stat.focus);
            break;

            case derivedStat.parryTH:
                output = getBasicStat(stat.focus);
            break;

            case derivedStat.turnFrequency:
                output = getBasicStat(stat.speed) + getBasicStat(stat.ingenuity);
            break;
        }

        return output;
    }

    public void tickBuffs()
    {
        List<Buff> buffsToRemove = new List<Buff>();
        foreach (Buff buff in buffs)
        {
            buff.tick();
            buff.duration--;
            if (buff.duration == 0)
            {
                buff.cleanup();
                buffsToRemove.Add(buff);
            }
        }

        foreach(Buff buff in buffsToRemove)
        {
            buffs.Remove(buff); 
        }
    }
}