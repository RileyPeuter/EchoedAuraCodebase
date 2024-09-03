using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFatigue : Buff
{
    public BlockFatigue(Character trgt) : base(trgt)
    {
        stackable = true;
        magnitude = 5;
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
        target.getDerivedStatObject(derivedStat.block).addAdditiveBuff(id, -magnitude);
    }

    public override void tick()
    {

    }

}

public class BlockDisabled : Buff
{
    public BlockDisabled(Character trgt) : base(trgt)
    {
        stackable = false;
        magnitude = 1;
        duration = 1;
        resourceName = "BlockDisabled";
        name = "Block Disabled";
        description = "This character is unable to block";
    }
    public override void cleanup()
    {
        target.reactionsAvailable.Add(reactionType.Block);
    }

    public override void start()
    {
        target.reactionsAvailable.Remove(reactionType.Block);
    }

    public override void tick()
    {

    }
}

public class DodgeFatigue : Buff
{
    public DodgeFatigue(Character trgt) : base(trgt)
    {

        stackable = true;
        magnitude = 5;
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
        target.getDerivedStatObject(derivedStat.dodge).addAdditiveBuff(id, -magnitude);
    }

    public override void tick()
    {

    }
}

public class DodgeDisabled : Buff
{
    public DodgeDisabled(Character trgt) : base(trgt)
    {
        stackable = false;
        magnitude = 1;
        duration = 1;
        resourceName = "DodgeDisabled";
        name = "Dodge Disabled";
        description = "This character is unable to dodge";
    }

    public override void cleanup()
    {
        target.reactionsAvailable.Add(reactionType.Dodge);
    }

    public override void start()
    {
        target.reactionsAvailable.Remove(reactionType.Dodge);
    }

    public override void tick()
    {

    }
}

public class ParryFatigue : Buff
{
    public ParryFatigue(Character trgt) : base(trgt)
    {

        stackable = true;
        magnitude = 5;
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
        target.getDerivedStatObject(derivedStat.parry).addAdditiveBuff(id, -magnitude);
    }

    public override void tick()
    {

    }
}

public class ParryDisabled: Buff
{
    public ParryDisabled(Character trgt) : base(trgt)
    {
        stackable = false;
        magnitude= 1;
        duration = 1;
        resourceName = "ParryDisabled";
        name = "Parry Disabled";
        description = "This character is unable to parry";
    }

    public override void cleanup()
    {
        target.reactionsAvailable.Add(reactionType.Dodge);
    }

    public override void start()
    {
        target.reactionsAvailable.Remove(reactionType.Dodge);
    }

    public override void tick()
    {

    }
}



