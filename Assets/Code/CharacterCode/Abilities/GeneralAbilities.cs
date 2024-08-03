using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeStrike : Ability
{
    public MeleeStrike() : base("MeleeStrike", ModType.Melee, 1, 1, 1, 2, AbilityType.Targeted, 11)
    {
        description = "A simple Melee Strike";

        baseDamage = 1;
    }
}

public class RangedStrike: Ability
{

    public RangedStrike() : base("RangedStrike", ModType.Ranged, 1, 1, 1, 1, AbilityType.Targeted, 12, false, 3)
    {
        baseDamage = 1;
        description = "A simple Ranged Strike";
    }
}

public class ShadowClone: Ability
{

    public ShadowClone() : base("ShadowClone", ModType.Ranged, 10, 10, 5, 2, AbilityType.Targeted)
    {

    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {

    }
}

public class Hide : Ability
{
    public Hide() : base("Hide", ModType.Ranged, 10, 10, 5, 2, AbilityType.Targeted)
    {
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {

    }
}
    

public class Recover: Ability
{

    public Recover() : base("Recover", ModType.Ranged, 10, 10, 5, 2, AbilityType.Targeted)
    {
    }
    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null )
    {

    }
}

