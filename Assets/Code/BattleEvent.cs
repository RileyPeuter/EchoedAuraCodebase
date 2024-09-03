using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleEvent
{
    //###MemberVariables###
    public BattleEventType eventType;
    public string subject = null;
    public string verb = null;
    public string target = null;
    public string result = null;

    //###Utilities###
    public override string ToString()
    {
        return (eventType + " " + subject + "  " + verb + " " + target + " " + result);
    }

    //###Constructors###
    public BattleEvent() { }

    public BattleEvent(BattleEventType nEventType, string nSubject = "", string nVerb = "", string nTarget = "", string nResult = "")
    {
        eventType = nEventType;
        subject = nSubject;
        verb = nVerb;
        target = nTarget;
        result = nResult;
    }
}

public enum BattleEventType
{
    Attack, Hit, Kill, Occupy, Interact, Time, EndTurn, Movement, React
}
