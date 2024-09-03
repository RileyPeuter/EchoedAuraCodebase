using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IceShuriken : Ability
{
    public IceShuriken() : base("IceShuriken", ModType.Ranged, 10, 3, 10, 3, AbilityType.Targeted, 1, false, 3)
    {
        description = "An improvised ranged spell. Hard to Block or Dodge";
        baseDamage = 3;
    }
}
    
public class LiquidSword : Ability
{
    public LiquidSword() : base("LiquidSword", ModType.Ranged, 10, 10, 5, 2, AbilityType.Self)
    {

    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {

    }
}

public class DemonMist : Ability
{
    public DemonMist() : base("DemonMist", ModType.Ranged, 10, 10, 5, 2,  AbilityType.Area)
    {

    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {

    }
}

public class IradensFrigidarium : Ability
{
    public IradensFrigidarium() : base("IradensFrigidarium", ModType.Ranged, 10, 10, 5, 2, AbilityType.Self)
    {

    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {

    }
}