using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFatigue : Buff
{
    public BlockFatigue(Character trgt) : base(trgt)
    {
        magnitude = 4;
        name = "Block Fatigue";
        duration = 1;
        resourceName = "BlockFatigue";
        description = "Reduction to Block. \n Applied for each time they block before their next turn.";
    }

    public override void cleanup()
    {
        target.getDerivedStatObject(derivedStat.block).removeBuff(id);
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.block).addAdditiveBuff(id, -4);
    }

    public override void tick()
    {

    }
}

public class DodgeFatigue : Buff
{
    public DodgeFatigue(Character trgt) : base(trgt)
    {
        magnitude = 4;
        duration = 1;
        resourceName = "DodgeFatigue";
        name = "Dodge Fatigue";
        description = "Reduction to Dodge. \n Applied for each time they Dodge before their next turn.";
    }

    public override void cleanup()
    {
        target.getDerivedStatObject(derivedStat.dodge).removeBuff(id);
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.dodge).addAdditiveBuff(id, -4);
    }

    public override void tick()
    {

    }
}

public class ParryFatigue : Buff
{
    public ParryFatigue(Character trgt) : base(trgt)
    {
        magnitude = 4;
        duration = 1;
        resourceName = "ParryFatigue";
        name = "Parry Fatigue";
        description = "Reduction to Parry. \n Applied for each time they Parry before their next turn.";
    }

    public override void cleanup()
    {
        target.getDerivedStatObject(derivedStat.parry).removeBuff(id);
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.parry).addAdditiveBuff(id, -4);
    }

    public override void tick()
    {

    }
}


