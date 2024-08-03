using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Inanimate : Buff
{
    public Inanimate(Character trgt) : base(trgt)
    {
        duration = 9999;
        resourceName = "Inanimate";
        name = "Inanimate";
        description = "Not a living thing. \nIf you can't hit this, I don't know what to tell you, ";
    }

    public override void cleanup()
    {

    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.block).addAdditiveBuff(id, -99);
        target.getDerivedStatObject(derivedStat.dodge).addAdditiveBuff(id, -99);
        target.getDerivedStatObject(derivedStat.parry).addAdditiveBuff(id, -99);
        target.getDerivedStatObject(derivedStat.maxHealthPoints).addAdditiveBuff(id, 100);
    }

    public override void tick()
    {

    }
}

