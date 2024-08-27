using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObjective : Objective
{
    List<ExWhyCell> cells;

    public override bool checkObjective(BattleEventType nBattleEventType, string nSubject = null, string nVerb = null, string nTarget = null, string nResult = null)
    {
        if (nBattleEventType != BattleEventType.Movement) { return false; }

        return StringCheck(nTarget);
    }

    public bool StringCheck(string stringToCheck)
    {
        foreach(ExWhyCell cell in cells)
        {
            if(cell.ToString() == stringToCheck) { return true; }
        }
        return false;
    }

    public MovementObjective(string nID, List<ExWhyCell> nCells, int nMaxCompletion) : base(nID, nMaxCompletion, BattleEventType.Movement)
    {
        cells = nCells;

    }

    public MovementObjective(string nID, int nMaxCompletion, BattleEventType nObjectiveType) : base(nID, nMaxCompletion, nObjectiveType)
    {
    }

    public MovementObjective(string nObjectiveID, string nDescription, int nMaxCompletion, BattleEventType nObjectiveType, GameObject nParent, float offset, int nCurrentCompletion = 0) : base(nObjectiveID, nDescription, nMaxCompletion, nObjectiveType, nParent, offset, nCurrentCompletion)
    {
    }


}
