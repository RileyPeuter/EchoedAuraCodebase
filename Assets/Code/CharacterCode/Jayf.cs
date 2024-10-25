using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jayf : Character   
{
    
    public Jayf() :base()
    {
        initiateBasicStats(2, 2, 2, 2, 2, 2);
        initiateDerivedStats();

        HealthPoints = getDerivedStat(derivedStat.maxHealthPoints);
        ManaPoints = getDerivedStat(derivedStat.maxManaPoints);
        CharacterName = "Jayf";
        resourceString = "Jayf";
        GeneralAbilities = new List<Ability>();
        CharacterAbilities = new List<Ability>();

        GeneralAbilities.Add(new RangedStrike());
        GeneralAbilities.Add(new MeleeStrike());
        CharacterAbilities.Add(new MagusPhantasma());
        CharacterAbilities.Add(new ArsAmagus());

        DefaultCAI = new AIJayf();
        reactionsAvailable.Remove(reactionType.Dodge);
    }
}

public class ArsAmagus : Ability
{
    public ArsAmagus(string nme, ModType mt, int bTH, int pTH, int dTH, int bMC, AbilityType abT, int anID = 0, bool sub = false, int rnge = 1) : base(nme, mt, bTH, pTH, dTH, bMC, abT, anID, sub, rnge)
    {
        
    }

    public ArsAmagus() : base("ArsAmagus", ModType.None, 999, 999, 999, 3,AbilityType.Self , 2, false, 3)
    {

    }


    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        caster.getCharacter().addBuff(new ShieldStrengthen(caster.getCharacter()));
        if(SOF != null) { 
        }
    }

}

public class MagusPhantasma : Ability
{
    public MagusPhantasma() : base("MagusPhantasma" , ModType.None, 3, 999 , 999, 999, AbilityType.Self, 1, false, 1)
    {

    }

    public MagusPhantasma(string nme, ModType mt, int bTH, int pTH, int dTH, int bMC, AbilityType abT, int anID = 0, bool sub = false, int rnge = 1) : base(nme, mt, bTH, pTH, dTH, bMC, abT, anID, sub, rnge)
    {
    }

    public override void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null, int reduction = 0)
    {
        BattleController.ActiveBattleController.spawnCharacter(GameObject.Instantiate(caster.gameObject), CharacterAllegiance.Enemey, ExWhy.activeExWhy.getClosestAvailableCell(target), new Jayf(), BattleController.ActiveBattleController.AIClusters[0]);
        BattleController.ActiveBattleController.spawnCharacter(GameObject.Instantiate(caster.gameObject), CharacterAllegiance.Enemey, ExWhy.activeExWhy.getClosestAvailableCell(target), new Jayf(), BattleController.ActiveBattleController.AIClusters[0]);
        //BattleController.ActiveBattleController.spawnCharacter(caster.gameObject, CharacterAllegiance.Enemey, ExWhy.activeExWhy.getClosestAvailableCell(target));
    }
}

