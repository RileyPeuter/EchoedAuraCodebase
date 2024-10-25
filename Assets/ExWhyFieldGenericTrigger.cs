using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ExWhyFieldGenericTrigger : ExWhyCellFieldTrigger
{
    BattleController BC;
    string fieldID;
    public ExWhyFieldGenericTrigger(ExWhy nMap, BattleController nBC, string nFieldID) : base(nMap)
    {
        BC = nBC;
    }

    public ExWhyFieldGenericTrigger(List<ExWhyCellField> fields, ExWhy nMap, BattleController nBC, string nFieldID) : base(fields, nMap)
    {
        BC = nBC;
    }

    public ExWhyFieldGenericTrigger(ExWhyCellField field, ExWhy nMap, BattleController nBC, string nFieldID) : base(field, nMap)
    {
        BC = nBC;
    }

    public override void hearEvent(BattleEvent nBattleEvent)
    {
        if (nBattleEvent.eventType != BattleEventType.Movement) { return; }

        if (BattleController.ActiveBattleController.getCharacterFromName(nBattleEvent.subject).GetAllegiance() != CharacterAllegiance.Controlled)
        {
            return;
        }

        foreach (ExWhyCell cell in getCells())
        {
            if (cell.ToString() == nBattleEvent.target)
            {
            }
        }
    }
}
