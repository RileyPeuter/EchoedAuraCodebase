using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum reactionType
{
    Dodge = 0,
    Block = 1,
    Parry = 2,
    None = 3
}

public class AttackAttempt 
{
    //###MemberVariables
    Ability ability;
    ModType modType;
    StandOffController SOC;

    int damage;

    BattleCharacterObject attacker;
    BattleCharacterObject attackee;
    reactionType reactionUsed = reactionType.None;
    int reactionMagnitude;
    int chance;
    int[,] hitMatrix;

    public List<reactionType> reactionsAvailalbe;

    //###Getters###
    public AttackAttempt(BattleCharacterObject atckr, BattleCharacterObject atcke, Ability ab)
    {
        attacker = atckr;
        attackee = atcke;
        ability = ab;
        modType = ability.getModType();
        //Make this cleaner when you can
        hitMatrix = new int[3, 3]{
        { atckr.getDodgeToHit() * ability.DodgeTH, atcke.getDodge(), (atckr.getDodgeToHit() * ability.DodgeTH) - atcke.getDodge()},
        { atckr.getBlockToHit() * ability.BlockTH, atcke.getBlock(),atckr.getBlockToHit() * ability.BlockTH - atcke.getBlock()},
        { atckr.getParryToHit() * ability.ParryTH, atcke.getParry(),atckr.getParryToHit() * ability.ParryTH - atcke.getParry()}};

        reactionsAvailalbe = attackee.getAvailableReactions();

    }

    public int[,] getHitMatrix()
    {
        return hitMatrix;
    }

    public int[] getMultipliers()
    {
        return new int[3] { ability.DodgeTH, ability.BlockTH, ability.ParryTH };
    }

    public Ability getAbility()
    {
        return ability;
    }

    public int getAbilityAniID()
    {
        return ability.getAniID();
    }

    public reactionType askReaction()
    {
        return attackee.selectReact();
    }

    public int attemptCast()
    {
        react(attackee.selectReact());
        chance = hitMatrix[(int)reactionUsed,2];
        
        int attackRoll =  + chance;
        return attackRoll;
    }

    public int getRandom()
    {
        return Random.Range(0, 10);
    }

    public int cast(StandOffSide SOF, int reduction = 0) 
    {
        ability.cast(attackee.getOccupying(), attacker, SOF, reduction);
        return damage;   
    }

    public int getDamage()
    {
        damage = ability.getDamage(attacker.getCharacter());
        return damage;
    }

    public void react(reactionType rt)
    {
        reactionUsed = rt;
    }

    public BattleCharacterObject getAttacker()
    {
        return attacker;
    }

    public BattleCharacterObject getAttackee()
    {
        return attackee;
    }

    public int getChange()
    {
        return chance;
    }

    public reactionType getReactionUsed()
    {
        return reactionUsed;
    } 

}
