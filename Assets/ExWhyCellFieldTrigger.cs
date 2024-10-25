using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyCellFieldTrigger : ExWhyCellField, BattleEventListener
{
    public ExWhyCellFieldTrigger(ExWhy nMap) : base(nMap)
    {
    }

    public ExWhyCellFieldTrigger(List<ExWhyCellField> fields, ExWhy nMap) : base(fields, nMap)
    {
    }

    public ExWhyCellFieldTrigger(ExWhyCellField field, ExWhy nMap) : base(field, nMap)
    {
    }

    public virtual void hearEvent(BattleEvent nBattleEvent)
    {

    }

    public virtual void onEnter()
    {
        
    }
}
