using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Area,
    Targeted,
    Self
}


public enum ModType
{
    Melee,
    Ranged,
    Mind,
    None
}

public abstract class Ability
{
    //### Member Variables ###

    //Flag for how to generate range
    protected RangeMode rangeMode = RangeMode.Simple;
    
    public Sprite abilityIcon;
    
    //Name of object that appears when you cast
    protected string effectObjectName = "hitEffect1";

    bool hasSubAbilities = false;


    int range = 1;
    public string name;
    public string description;
    public string resourceName;

    protected int baseDamage;

    int blockTH;
    int parryTH;
    int dodgeTH;

    public AbilityType abilityType;
    ModType modType;
    int animationID;
    int baseManaCost;


    //### Getters ###
    public string getEffectObjectName()
    {
        return effectObjectName;
    }

    public RangeMode GetRangeMode()
    {
        return rangeMode;
    }

    Sprite loadSprite()
    {
        abilityIcon = Resources.Load<Sprite>("AbilityIcons/" + resourceName);
        return abilityIcon;
    }

    public int getAniID()
    {
        return animationID;
    }

    public virtual int GetRange()
    {
        return range;
    }

    //###Setters###

    public void setRangeMode(RangeMode nRangeMode)
    {
        rangeMode = nRangeMode;
    }

    //###Utilities###
    public virtual bool isCastable(BattleCharacterObject BCO)
    {
        if (BCO.getManaFlow() < baseManaCost) { return false; }
        return true;
    }

    public string getTypeForUI()
    {
        if (modType == ModType.Melee) { return "Me"; }
        if (modType == ModType.Ranged) { return "Rn"; }
        if (modType == ModType.Mind) { return "Mi"; }
        return "t";
    }

    public virtual string getCostString()
    {
        return ("M:" + baseManaCost);
    }
    public int BlockTH { get => blockTH; set => blockTH = value; }
    public int ParryTH { get => parryTH; set => parryTH = value; }
    public int DodgeTH { get => dodgeTH; set => dodgeTH = value; }

    public virtual void cast(ExWhyCell target, BattleCharacterObject caster, StandOffSide SOF = null)
    {
        target.occupier.takeDamage(getDamage(caster.getCharacter()));
    }

    public virtual void spendMana(BattleCharacterObject BCO)
    {
        BCO.spendMana(baseManaCost);
    }

    public ModType getModType() { return modType; }

    public int getDamage(Character caster)
    {
        if (baseDamage == 0)
        {
            return 0;
        }
        switch (modType)
        {
            case ModType.Melee:
                return baseDamage + caster.getDerivedStat(derivedStat.meleeBonus);
                break;

            case ModType.Mind:
                return baseDamage + caster.getDerivedStat(derivedStat.magicBonus);
                break;


            case ModType.Ranged:
                return baseDamage + caster.getDerivedStat(derivedStat.rangedBonus);
                break;
        }
        return baseDamage * caster.getDerivedStat(derivedStat.rangedBonus);
    }

    //###Constructors###

    protected Ability(string nName, ModType nModType, int nBlockTH, int nParryTH, int nDodgeTH, int nBaseManaCost, AbilityType nAbilityType, int nAnimationID = 0, bool nHasSubAbilities = false, int nRange = 1)
    {
        name = nName;
        modType = nModType;
        blockTH = nBlockTH;
        parryTH = nParryTH;
        dodgeTH = nDodgeTH;
        baseManaCost = nBaseManaCost;
        abilityType = nAbilityType;
        animationID = nAnimationID;
        hasSubAbilities = nHasSubAbilities;
        resourceName = nName;
        loadSprite();
        range = nRange;
    }
}
