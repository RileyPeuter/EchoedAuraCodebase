using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility 
{
    Character user;
    public abstract void activate(BattleCharacterObject target, StandOffSide SOF);

}
