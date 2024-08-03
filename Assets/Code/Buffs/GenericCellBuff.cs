using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCellBuff : CellBuff
{
    //###MemberVeriables###
    public int dodgeBonus;
    public int blockBonus;
    public int parryBonus;

    //###Overrides###
    public override void cleanup()
    {
        target.getDerivedStatObject(derivedStat.dodge).removeBuff(id);
        target.getDerivedStatObject(derivedStat.block).removeBuff(id);
        target.getDerivedStatObject(derivedStat.parry).removeBuff(id);
    }

    public override void start()
    {
        id = Buff.getID();
        target.getDerivedStatObject(derivedStat.dodge).addAdditiveBuff(id, dodgeBonus);
        target.getDerivedStatObject(derivedStat.block).addAdditiveBuff(id, blockBonus);
        target.getDerivedStatObject(derivedStat.parry).addAdditiveBuff(id, parryBonus);
    }

    public override string getDescriptionString()
    {
        return ("D: " + dodgeBonus + "B: " + blockBonus + "P: " + parryBonus);
    }

    public override GameObject getDisplayGameObject()
    {
        return Resources.Load<GameObject>("UIElements/uI_CellEffects_Panel 1");
    }

    public override void tick(){}

    //###Constructors###
    public GenericCellBuff(Character nTarget, int nDodgeBonus, int nBlockBonus, int nParryBonus) : base(nTarget)
    {
        dodgeBonus = nDodgeBonus;
        blockBonus = nBlockBonus;
        parryBonus = nParryBonus;
    }

    public GenericCellBuff(int nDodgeBonus, int nBlockBonus, int nParryBonus)
    {
        dodgeBonus = nDodgeBonus;
        blockBonus = nBlockBonus;
        parryBonus = nParryBonus;
    }

    public override CellBuff Clone(Character nTarget)
    {
        return new GenericCellBuff(nTarget, dodgeBonus, blockBonus, parryBonus);
    }
}
