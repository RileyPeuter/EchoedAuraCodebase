using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CellBuff : Buff
{
    protected CellBuff(Character nTarget) : base(nTarget)
    {
        
    }

    protected CellBuff() { }

    public abstract CellBuff Clone(Character nTarget);

    public abstract string getDescriptionString();
        
}

