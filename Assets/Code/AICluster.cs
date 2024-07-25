using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroupAIModes
{
    Passive,
    Retreat,
    Standard,
    FullRecklessAttack,
    Special 
}

public class AICluster 
{
    //###MemberVariables###
    GroupAIModes GroupMode = GroupAIModes.Standard;

    public List<BattleCharacterAI> ClusterMembers;

    int mobility = -1;
    int murderousIntent = 1;
    int gaps = 0;

    //###Utilities###
    public virtual void setMemeberModes()
    {
        StandOffController.print(getTankedCount());
        if (GroupMode == GroupAIModes.Passive)
        {
            setAllModes(AIMode.Passive);
        }

        if (GroupMode == GroupAIModes.FullRecklessAttack)
        {
            setAllModes(AIMode.Passive);
        }

        if (GroupMode == GroupAIModes.Standard)
        {
            setAllModes(AIMode.RecklessAttack);
            if (gaps < (ClusterMembers.Count - getTankedCount()))
            {
                StandOffController.print("bro");
                makeTanked((ClusterMembers.Count) - gaps - getTankedCount());
            }
        }
    }

    public void makeTanked(int amount)
    {
        int amountLeft = amount;

        foreach (BattleCharacterAI BCAI in ClusterMembers)
        {


            if (amountLeft > 0 && !BCAI.getTankedUp())
            {
                BCAI.setAIMode(AIMode.TankUp);
                amountLeft = amountLeft - 1;
            }
        }
    }

    public int getTankedCount()
    {
        int count = 0;
        foreach (BattleCharacterAI BCAI in ClusterMembers)
        {
            if (BCAI.getTankedUp())
            {
                count = count + 1;
            }
        }
        return count;
    }

    void setAllModes(AIMode mode)
    {
        foreach (BattleCharacterAI BCAI in ClusterMembers)
        {
            BCAI.setAIMode(mode);
        }
    }

    //###Constructor###
    public AICluster()
    {

        ClusterMembers = new List<BattleCharacterAI>();
    }

    public AICluster(int mob, int murd, int gps)
    {
        ClusterMembers = new List<BattleCharacterAI>();
        mobility = mob;
        murderousIntent = murd;
        gaps = gps;
    }
}
