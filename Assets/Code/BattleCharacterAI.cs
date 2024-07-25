using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIMode{
    Passive,
    RecklessAttack,
    TankUp,
    Support
    }


public abstract class BattleCharacterAI
{
    //###MemberVariables###

    protected BattleCharacterObject BCO;
    protected int murderousIntent = 1;
    protected bool tankedUp;

    protected AIMode mode = AIMode.Passive;

    protected Ability overrideAbility;

    //###Getters###
    public Ability getOverrideAbility()
    {
        return overrideAbility;
    }
    public bool getTankedUp()
    {
        return tankedUp;
    }

    public virtual reactionType getReaction()
    {
        return getRandomReact();
    }


    //###Setters###
    public void setOverrideAbility(Ability nOverrideAbility) 
    {
        overrideAbility = nOverrideAbility;
    }

    public void setAIMode(AIMode mde)
    {
        mode = mde;
    }

    //###Utilities###

    public static reactionType getRandomReact()
    {
        //Bro, why the fuck would you make Round return a float
        switch (Random.Range(0, 3))
        {
            case 0:
                return reactionType.Dodge;
            case 1:

                return reactionType.Block;
            case 2:
                return reactionType.Parry;
        }
        return reactionType.Dodge;
    }

    public List<Ability> getAvailableAbilities()
    {
        List<Ability> output = new List<Ability>();
        foreach (Ability ability in BCO.getAllAbilities())
        {
            if (ability.isCastable(BCO)) { output.Add(ability); }
        }
        return output;
    }

    public virtual ExWhyCell moveTowardsEnemy(bool requiresAbailable = false)
    {
        return AbilityRange.getClosestCell(BattleController.ActiveBattleController.getAllAllegiance(CharacterAllegiance.Controlled)[0].getOccupying(), BattleController.ActiveBattleController.AR.findCellsInRange(RangeMode.Move), requiresAbailable);
    }
    public ExWhyCell moveTowardsEnemy(BattleCharacterObject target, bool requiresAbailable = false)
    {
        return AbilityRange.getClosestCell(BattleController.ActiveBattleController.getAllAllegiance(CharacterAllegiance.Controlled)[0].getOccupying(), BattleController.ActiveBattleController.AR.findCellsInRange(RangeMode.Move), requiresAbailable);
    }

    public ExWhyCell GetMurderousTarget(List<BattleCharacterObject> candidates, int murderousIntent)
    {
        List<BattleCharacterObject> BCOs = new List<BattleCharacterObject>();
        if (candidates.Count == 0) { return null; }

        int highestHealth = 0;
        foreach (BattleCharacterObject candidate in candidates)
        {
            if (candidate.getCurrentHealthPoints() > highestHealth) { highestHealth = candidate.getCurrentHealthPoints(); }
        }

        foreach (BattleCharacterObject candidate in candidates)
        {
            if (candidate.getCurrentHealthPoints() + murderousIntent >= highestHealth && candidate.GetAllegiance() != CharacterAllegiance.Enemey)
            {
                BCOs.Add(candidate);
            }
        }

        if (BCOs.Count == 0) { return null; }

        return BCOs[Random.Range(0, BCOs.Count)].getOccupying();
    }

    public abstract Ability getAbility();

    public abstract ExWhyCell getTarget(Ability ability, AbilityRange AR);

    //###Initializer###
    public void initialize(BattleCharacterObject nBCO)
    {
        BCO = nBCO;
    }
}
