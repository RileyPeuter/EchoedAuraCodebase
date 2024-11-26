using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility
{
    Agency agency;
    string name;
    derivedStat affectedStat;
    int level = 0;
    int costForNext;
    string description;


//###Getters###
    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public int getLevel()
    {
        return level; 
    }

    public int getCost()
    {
        return costForNext;
    }

    public Facility(string nName, string nDescription, int nCost, derivedStat nAffectedStat, Agency nAgency)
    {
        name = nName;
        description = nDescription;
        costForNext = nCost;
        affectedStat = nAffectedStat;
        agency = nAgency;
    }

    public bool attemptBuild()
    {
        if(agency.getGold() >= costForNext)
        {
            build();
            agency.addGold(-costForNext);
            return true;
        }
        return false;
    }

    public bool getBuildable()
    {
        if(agency.getGold() >= costForNext)
        {
            return true;
        }
        return false;
    }

    public virtual void build()
    {
        foreach (StoredCharacterObject character in agency.getCharacters())
        {
            character.GetCharacter().getDerivedStatObject(affectedStat).addAdditiveBuff(Buff.getID(), 1);
        }
    } 
}
