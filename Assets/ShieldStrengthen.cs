using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStrengthen : Buff
{
    public ShieldStrengthen(Character trgt) : base(trgt)
    {
        duration = 1;
        resourceName = "BlockStrengthen";
        name = "Fortified";
        description = "Increase Block. \nGenerally good to attack other characters";
    }

    public override void cleanup()
    {
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.block).addAdditiveBuff(id, 5);
    }

    public override void tick()
    {

    }
}