using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fray : Character
{
    public Fray() : base()
    {
        initiateBasicStats(1, 2, 3, 1, 2, 4);
        initiateDerivedStats();
        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Fray";
        resourceString = "Fray";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new RangedStrike());
        GeneralAbilities.Add(new MeleeStrike());

        CharacterAbilities.Add(new ShadowGrab());
    }
}

public class ShadowGrab : Ability
{
    public ShadowGrab(string nme, ModType mt, int bTH, int pTH, int dTH, int bMC, AbilityType abT, int anID = 0, bool sub = false, int rnge = 1) : base(nme, mt, bTH, pTH, dTH, bMC, abT, anID, sub, rnge)
    {
        effectObjectName = "ShadowHoldEffect";
        description = "Non damaging anti-dodge spell";
    }

    public ShadowGrab() : base("ShadowGrab", ModType.Mind, 4, 4, 6, 3,AbilityType.Targeted, 1)
    {
        description = "Non damaging anti-dodge spell";
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null){
        effectObjectName = "ShadowHoldEffect";

        ShadowHold SH = new ShadowHold(target.occupier.getCharacter());
        target.occupier.getCharacter().addBuff(SH);
        SOF.effectMessage("Shadow Hold", SH.getSprite());
        StandOffController.print("Here");
    }
}


public class ShadowHold : Buff
{
    public ShadowHold(Character trgt) : base(trgt)
    {
        name = "Shadow Hold";
        magnitude = 6;
        duration = 4;
        resourceName = "ShadowHold";
        description = "Reduces Dodge. \n Heavy shadows stick to the users";
    }

    public override void cleanup()
    {
        target.getDerivedStatObject(derivedStat.dodge).removeBuff(id);
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.dodge).addAdditiveBuff(id, -6);
    }

    public override void tick()
    {

    }
}
