using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyCellVisionField : ExWhyCellFieldTrigger
{
    AICluster cluster;    
    public ExWhyCellVisionField(ExWhy nMap, AICluster nCluster) : base(nMap)
    {
        cluster = nCluster;
    }

    public ExWhyCellVisionField(List<ExWhyCellField> fields, ExWhy nMap) : base(fields, nMap)
    {
    }

    public ExWhyCellVisionField(ExWhyCellField field, ExWhy nMap) : base(field, nMap)
    {
    }

    public override void hearEvent(BattleEvent nBattleEvent)
    {
        if(nBattleEvent.eventType != BattleEventType.Movement){return;}
        
        if(BattleController.ActiveBattleController.getCharacterFromNameID(nBattleEvent.subject).GetAllegiance() != CharacterAllegiance.Controlled)
        {
            return;
        }

        foreach (ExWhyCell cell in getCells()) {
            if (cell.ToString() == nBattleEvent.target)
            {
                cluster.groupAlert(BattleController.ActiveBattleController.getTurnTime());
                despawnVisuals();
            }
        }
    }   
}
