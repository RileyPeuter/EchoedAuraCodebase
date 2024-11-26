using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class CellBuff : Buff
{

    protected CellBuff(Character nTarget) : base(nTarget)
    {
        visible = false;        
    }

    protected CellBuff() { }

    public abstract CellBuff Clone(Character nTarget);

    public abstract string getDescriptionString();
        
    public virtual GameObject getDisplayGameObject()
    {
        return Resources.Load<GameObject>("UIElements/uI_CellEffects_Panel"); 
    }
}

