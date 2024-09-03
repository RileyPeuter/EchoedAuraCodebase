using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Scripting.APIUpdating;

public enum AIMode{
    Passive,
    RecklessAttack,
    TankUp,
    Support
    }


public abstract class BattleCharacterAI
{
    //###MemberVariables###

    protected bool moved = false;

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
        Assert.IsFalse(BCO.getAvailableReactions().Contains(reactionType.None));

        reactionType output = reactionType.None;

        if (BCO.getAvailableReactions().Count > 0)
        {
            while (!BCO.getAvailableReactions().Contains(output))
            {
                output = getRandomReact();
            }
        }

        return output;
    }

    public reactionType getDistributedReaction()
    {
        int dodgeReaction = BCO.getDodge() + 1;
        int blockReaction = BCO.getBlock() + 1;
        int parryReaction = BCO.getParry() + 1;

        int roll = Random.Range(0, dodgeReaction + blockReaction + parryReaction);

        if(roll < dodgeReaction)
        {
            return reactionType.Dodge;
        }

        if(roll < dodgeReaction + blockReaction)
        {
            return reactionType.Block;
        }

        return reactionType.Parry;

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

    public virtual ExWhyCell moveTowardsAlliedAndControlled(bool requiresAbailable = false)
    {
        List<CharacterAllegiance> alliegances = new List<CharacterAllegiance>();
        alliegances.Add(CharacterAllegiance.Allied);
        alliegances.Add(CharacterAllegiance.Controlled);
        return AbilityRange.getClosestCell(BattleController.ActiveBattleController.getAllAlliegiances(alliegances)[0].getOccupying(), BattleController.ActiveBattleController.AR.findCellsInRange(RangeMode.Move), requiresAbailable);
    }

    public virtual ExWhyCell getTarget(List<ExWhyCell> range)
    {
        foreach (ExWhyCell cell in range)
        {
            if (cell.occupier != null)
            {
                if (cell.occupier.GetAllegiance() == CharacterAllegiance.Allied || cell.occupier.GetAllegiance() == CharacterAllegiance.Controlled)
                {
                    return cell;
                }
            }
        }
        return null;
    }

        public virtual ExWhyCell getTarget(List<BattleCharacterObject> range)
    {
        foreach (BattleCharacterObject cell in range)
        {
                if (cell.GetAllegiance() == CharacterAllegiance.Allied || cell.GetAllegiance() == CharacterAllegiance.Controlled)
                {
                    return cell.getOccupying();
                }
        }
        return null;
    }

    public virtual ExWhyCell moveTowardsControlled(bool requiresAbailable = false)
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
