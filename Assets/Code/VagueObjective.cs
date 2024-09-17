using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Networking.UnityWebRequest;

public class VagueObjective : Objective
{
    //This variation of Objective is for when you want to set objectives for certain types, like "kill 4 of a certain type of enemy".
    //Because their ID's will be different, we need to see if  things are contained instead of equal
    public VagueObjective(string nID, int nMaxCompletion, BattleEventType nObjectiveType) : base(nID, nMaxCompletion, nObjectiveType)
    {
    }

    public override bool checkObjective(BattleEventType nBattleEventType, string nSubject = null, string nVerb = null, string nTarget = null, string nResult = null)
    {
        //This block just checks if the event checks if something is equal to the objecitve
        if (objectiveType != nBattleEventType) { return false; }
        if (subject != null && !nSubject.Contains(subject)) { return false; }
        if (verb != null && !nVerb.Contains(verb)) { return false; }
        if (target != null && !nTarget.Contains(target)) { return false; }
        if (result != null && !nResult.Contains(result) ) { return false; }

        if (testModifier(modifier, nResult, modifierThreshold))
        {
            return true;
        }

        return false;
    }
}
