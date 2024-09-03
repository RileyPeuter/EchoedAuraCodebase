using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character {
    //###MemberVariables

    public List<reactionType> reactionsAvailable = new List<reactionType>() {reactionType.Block, reactionType.Dodge, reactionType.Parry};


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
            new DerivedStat(derivedStat.movement, getBasicStat(stat.strength), getBasicStat(stat.speed)),
            new DerivedStat(derivedStat.maxHealthPoints, getBasicStat(stat.vitality), 0, 5),
            new DerivedStat(derivedStat.maxManaPoints, getBasicStat(stat.vitality),getBasicStat(stat.focus), 20),
            new DerivedStat(derivedStat.manaFlow, getBasicStat(stat.speed),getBasicStat(stat.focus)),
            new DerivedStat(derivedStat.meleeBonus, getBasicStat(stat.strength),getBasicStat(stat.speed)),
            new DerivedStat(derivedStat.rangedBonus, getBasicStat(stat.precision)),
            new DerivedStat(derivedStat.magicBonus, getBasicStat(stat.ingenuity),getBasicStat(stat.focus)),
            new DerivedStat(derivedStat.block, getBasicStat(stat.strength),getBasicStat(stat.vitality), -5),
            new DerivedStat(derivedStat.dodge, getBasicStat(stat.speed),getBasicStat(stat.ingenuity), -5),
            new DerivedStat(derivedStat.parry, getBasicStat(stat.precision),getBasicStat(stat.focus), -5),
            new DerivedStat(derivedStat.blockTH, getBasicStat(stat.strength)),
            new DerivedStat(derivedStat.dodgeTH, getBasicStat(stat.speed)),
            new DerivedStat(derivedStat.parryTH, getBasicStat(stat.focus)),
            new DerivedStat(derivedStat.turnFrequency, getBasicStat(stat.speed), getBasicStat(stat.ingenuity))
        };
    }

    public void tickBuffs()
    {
        List<Buff> buffsToRemove = new List<Buff>();
        foreach (Buff buff in buffs)
        {
            buff.tick();
            buff.duration--;
            if (buff.duration <= 0)
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